using DotaPlus.Contracts.Services;
using DotaPlus.Core.Contracts.Services;
using DotaPlus.ViewModels;
using Microsoft.UI.Xaml;
using System;
using System.Threading.Tasks;

namespace DotaPlus.Activation
{
    public class DefaultActivationHandler : ActivationHandler<LaunchActivatedEventArgs>
    {
        private readonly INavigationService _navigationService;
        private readonly IDotaConstants _rc;

        public DefaultActivationHandler(INavigationService navigationService, IDotaConstants rc)
        {
            _navigationService = navigationService;
            _rc = rc;
        }

        protected override async Task HandleInternalAsync(LaunchActivatedEventArgs args)
        {
            await _rc.Initialize();
            _navigationService.NavigateTo(typeof(HeroesViewModel).FullName, args.Arguments);
            await Task.CompletedTask;
        }

        protected override bool CanHandleInternal(LaunchActivatedEventArgs args)
        {
            // None of the ActivationHandlers has handled the app activation
            return _navigationService.Frame.Content == null;
        }
    }
}
