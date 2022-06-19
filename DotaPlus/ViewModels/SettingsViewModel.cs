using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotaPlus.Contracts.Services;
using DotaPlus.Contracts.ViewModels;
using DotaPlus.Helpers;
using Microsoft.UI.Xaml;
using System;
using System.Reflection;

namespace DotaPlus.ViewModels
{
    public partial class SettingsViewModel : ObservableRecipient, INavigationAware
    {
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly ILocalSettingsService _localSettingsService;
        [ObservableProperty]
        private string _versionDescription;

        [ObservableProperty]
        private ElementTheme _elementTheme;

        [ObservableProperty]
        private long _steamId;

        [RelayCommand]
        public void SwitchTheme(ElementTheme theme)
        {
            ElementTheme = theme;
        }

        async partial void OnElementThemeChanged(ElementTheme value)
        {
            await _themeSelectorService.SetThemeAsync(value);
        }

        async partial void OnSteamIdChanged(long value)
        {
            await _localSettingsService.SaveSettingAsync(nameof(SteamId), value);
        }

        public SettingsViewModel(IThemeSelectorService themeSelectorService, ILocalSettingsService localSettingsService)
        {
            _themeSelectorService = themeSelectorService;
            _localSettingsService = localSettingsService;
        }

        private string GetVersionDescription()
        {
            var appName = "AppDisplayName".GetLocalized();
            var version = Assembly.GetExecutingAssembly().GetName().Version;

            return $"{appName} - {version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }

        public async void OnNavigatedTo(object parameter)
        {
            ElementTheme = _themeSelectorService.Theme;
            SteamId = await _localSettingsService.ReadSettingAsync<long>(nameof(SteamId));
            VersionDescription = GetVersionDescription();
        }

        public void OnNavigatedFrom()
        {
        }
    }
}
