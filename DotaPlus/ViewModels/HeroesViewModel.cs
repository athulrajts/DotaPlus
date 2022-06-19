using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.UI;
using DotaPlus.Contracts.Services;
using DotaPlus.Core.Contracts.Services;
using DotaPlus.Core.Models;
using DotaPlus.Helpers;
using System;
using Attribute = DotaPlus.Core.Models.Attribute;

namespace DotaPlus.ViewModels;

public partial class HeroesViewModel : ObservableObject
{
    private readonly INavigationService _navigationService;

    public HeroesViewModel(IHeroDetailsService heroDetailsService, INavigationService navigationService)
    {
        Heroes = new(heroDetailsService.Heroes);
        _navigationService = navigationService;
    }

    public AdvancedCollectionView Heroes { get; }
    public bool IsAttributeSelectionEnabled => string.IsNullOrEmpty(SearchText);

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsAttributeSelectionEnabled))]
    private string _searchText;

    [ObservableProperty]
    private bool _filterByStr;
    
    [ObservableProperty]
    private bool _filterByAgi;

    [ObservableProperty]
    private bool _filterByInt;

    [RelayCommand]
    public void HeroSelected(HeroModel hero)
    {
        _navigationService.NavigateTo<HeroDetailsViewModel>(hero);
    }

    partial void OnSearchTextChanged(string value)
    {
        if (!IsAttributeSelectionEnabled)
        {
            SetFilter(x => x.LocalizedName.ToLowerInvariant().Contains(value.ToLowerInvariant()));
        }
        else
        {
            if (FilterByStr) FilterStrHeroes();
            else if (FilterByAgi) FilterAgiHeroes();
            else if (FilterByInt) FilterIntHeroes();
            else ClearFilter();
        }
    }

    partial void OnFilterByStrChanged(bool value)
    {
        if (value)
        {
            FilterStrHeroes();
        }
        else
        {
            if (!FilterByAgi && !FilterByInt)
            {
                ClearFilter();
            }
        }
    }
    
    partial void OnFilterByAgiChanged(bool value)
    {
        if (value)
        {
            FilterAgiHeroes();
        }
        else
        {
            if (!FilterByStr && !FilterByInt)
            {
                ClearFilter();
            }
        }
    }

    partial void OnFilterByIntChanged(bool value)
    {
        if (value)
        {
            FilterIntHeroes();
        }
        else
        {
            if (!FilterByStr && !FilterByAgi)
            {
                ClearFilter();
            }
        }
    }

    
    
    private void FilterAgiHeroes()
    {
        SetFilter(x => x.PrimaryAttribute == Attribute.Agility);
        FilterByStr = false;
        FilterByInt = false;
    }

    private void FilterStrHeroes()
    {
        SetFilter(x => x.PrimaryAttribute == Attribute.Strength);
        FilterByAgi = false;
        FilterByInt = false;
    }

    private void FilterIntHeroes()
    {
        SetFilter(x => x.PrimaryAttribute == Attribute.Intelligence);
        FilterByStr = false;
        FilterByAgi = false;
    }

    private void SetFilter(Predicate<HeroModel> filter)
    {
        Heroes.Filter = model => filter(model as HeroModel);
        Heroes.RefreshFilter();
    }

    private void ClearFilter()
    {
        Heroes.Filter = _ => true;
        Heroes.RefreshFilter();
    }
}   
