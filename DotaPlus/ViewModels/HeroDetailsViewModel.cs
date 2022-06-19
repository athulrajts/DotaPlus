using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotaPlus.Contracts.ViewModels;
using DotaPlus.Core.Contracts.Services;
using DotaPlus.Core.Helpers;
using DotaPlus.Core.Models;
using DotaPlus.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace DotaPlus.ViewModels;

public partial class HeroDetailsViewModel : ObservableObject, INavigationAware
{
    private readonly IHeroDetailsService _heroDetailsService;
    private HeroModel _hero;

    [ObservableProperty]
    private string _abilityDemoUrl;

    [ObservableProperty]
    private AbilityModel _selectedAbility;

    public string Lore => _hero.Lore;
    public string LocalizedName => _hero.LocalizedName;
    public string AttackRate => _hero.AttackRate.ToString();
    public string AttackRange => _hero.AttackRange.ToString();
    public string ProjectileSpeed => _hero.ProjectileSpeed.ToString();
    public string Armor => _hero.BaseArmor.ToString();
    public string AttackDamage => _hero.GetAttackDamageString();
    public string MagicResistance => _hero.BaseMagicResistance.ToString();
    public string MovementSpeed => _hero.MovementSpeed.ToString();
    public string TurnRate => _hero.TurnRate.ToString();
    public string Strength => _hero.BaseStrength.ToString();
    public string StrengthGain => _hero.StrengthGain.ToString();
    public string Agility => _hero.BaseAgility.ToString();
    public string AgilityGain => _hero.AgilityGain.ToString();
    public string Intelligence => _hero.BaseIntelligence.ToString();
    public string IntelligenceGain => _hero.IntelligenceGain.ToString();
    public string AnimatedPortraitUrl => DotaResourceHelper.GetAnimatedHeroPortrait(_hero.Name);
    public string PortraitUrl => DotaResourceHelper.GetHeroPortrait(_hero.Name);
    public IEnumerable<AbilityModel> Abilities => _hero?.Abilities.Where(x => x.HasDemo);
    public bool IsAnyAbilitySelcted => _selectedAbility is not null;
    public Attribute PrimaryAttribute => _hero.PrimaryAttribute;
    public string Health => _hero.GetHealth().ToString();
    public string Mana => _hero.GetManaString();
    public string HealthRegen => _hero.GetHealthRegenString();
    public string ManaRegen => _hero.GetManaRegenString();

    public bool SelectedAbilityHasDamageType => SelectedAbility.DamageType.Any();
    public bool SelectedAbilityHasCd => SelectedAbility.Cooldown is null ? false : SelectedAbility.Cooldown.Any();
    public bool SelectedAbilityHasMc => SelectedAbility.ManaCost is null ? false : SelectedAbility.ManaCost.Any();


    public HeroDetailsViewModel(IHeroDetailsService heroDetailsService)
    {
        _heroDetailsService = heroDetailsService;
    }

    partial void OnSelectedAbilityChanged(AbilityModel value)
    {
        AbilityDemoUrl = DotaResourceHelper.GetAbilityDemo(value.Name);
    }


    public void OnNavigatedFrom() { }

    public void OnNavigatedTo(object parameter)
    {
        if(parameter is HeroModel hero)
        {
            _hero = hero;
            SelectedAbility = Abilities.First();
            OnPropertyChanged(string.Empty);
        }
    }
}
