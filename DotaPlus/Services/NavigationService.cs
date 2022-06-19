using DotaPlus.Contracts.Services;
using DotaPlus.Contracts.ViewModels;
using DotaPlus.Core;
using DotaPlus.Helpers;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace DotaPlus.Services
{
    // For more information on navigation between pages see
    // https://github.com/microsoft/TemplateStudio/blob/main/docs/WinUI/navigation.md
    public class NavigationService : INavigationService
    {
        private readonly IPageService _pageService;
        private object _lastParameterUsed;
        private Frame _frame;

        public event NavigatedEventHandler Navigated;

        public Frame Frame
        {
            get
            {
                if (_frame == null)
                {
                    _frame = App.MainWindow.Content as Frame;
                    RegisterFrameEvents();
                }

                return _frame;
            }

            set
            {
                UnregisterFrameEvents();
                _frame = value;
                RegisterFrameEvents();
            }
        }

        public bool CanGoBack => Frame.CanGoBack;

        public NavigationService(IPageService pageService)
        {
            _pageService = pageService;
        }

        private void RegisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated += OnNavigated;
            }
        }

        private void UnregisterFrameEvents()
        {
            if (_frame != null)
            {
                _frame.Navigated -= OnNavigated;
            }
        }

        public bool GoBack()
        {
            if (CanGoBack)
            {
                var vmBeforeNavigation = _frame.GetPageViewModel();
                _frame.GoBack();
                if (vmBeforeNavigation is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedFrom();
                }

                return true;
            }

            return false;
        }

        public bool NavigateTo(string pageKey, object parameter = null, bool clearNavigation = false)
        {
            var pageType = _pageService.GetPageType(pageKey);

            if (_frame.Content?.GetType() != pageType || (parameter != null && !parameter.Equals(_lastParameterUsed)))
            {
                _frame.Tag = clearNavigation;
                var vmBeforeNavigation = _frame.GetPageViewModel();
                var navigated = _frame.Navigate(pageType, parameter);
                if (navigated)
                {
                    _lastParameterUsed = parameter;
                    if (vmBeforeNavigation is INavigationAware navigationAware)
                    {
                        navigationAware.OnNavigatedFrom();
                    }
                }

                return navigated;
            }

            return false;
        }

        public void CleanNavigation()
            => _frame.BackStack.Clear();

        private void OnNavigated(object sender, NavigationEventArgs e)
        {
            if (sender is Frame frame)
            {
                bool clearNavigation = (bool)frame.Tag;
                if (clearNavigation)
                {
                    frame.BackStack.Clear();
                }

                var vm = frame.GetPageViewModel();

                if (vm is null)
                {
                    if(frame.Content.GetType().GetProperty("ViewModel") is not PropertyInfo pInfo)
                    {
                        return;
                    }

                    var viewInterface = typeof(IViewFor<>);
                    var viewType = viewInterface.MakeGenericType(pInfo.PropertyType);

                    if(viewType.IsAssignableFrom(frame.Content.GetType()))
                    {
                        SetViewModel(pInfo.PropertyType, frame.Content, ref vm);
                    }
                }

                if (vm is INavigationAware navigationAware)
                {
                    navigationAware.OnNavigatedTo(e.Parameter);
                }

                Navigated?.Invoke(sender, e);
            }
        }

        private void SetViewModelGeneric<T>(IViewFor<T> view, T vm)
            where T : class, INotifyPropertyChanged
        {
            view.ViewModel = vm;
        }

        private void SetViewModel(Type vmType, object view, ref object vm)
        {
            var func = GetType().GetMethod(nameof(SetViewModelGeneric), BindingFlags.NonPublic | BindingFlags.Instance).MakeGenericMethod(vmType);
            vm = App.GetService(vmType);
            func.Invoke(this, new[] { view, vm});
        }
    }
}
