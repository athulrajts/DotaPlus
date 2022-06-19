using DotaPlus.Core.Models;
using DotaPlus.Core.Models.Internal;
using System.Collections.Generic;
using System.Linq;

namespace DotaPlus.Core.Helpers;

internal static class ConstantToModelHelper
{
    internal static void UpdateHeroInfo(this HeroModel model, _Hero hero)
    {
        model.Id = hero.Id;
        model.Name = hero.Name;
        model.LocalizedName = hero.LocalizedName;
        model.PrimaryAttribute = hero.PrimaryAttribute switch
        {
            "agi" => Attribute.Agility,
            "int" => Attribute.Intelligence,
            "str" => Attribute.Strength,
            _ => throw new System.ArgumentException(null, nameof(hero.PrimaryAttribute))
        };
    
        model.AttackType = System.Enum.Parse<AttackType>(hero.AttackType);
        model.Roles = hero.Roles.Select(x => System.Enum.Parse<Role>(x)).ToList();
        model.Image = hero.GetImageUrl();
        model.Icon = hero.Icon;
        model.BaseHealth = hero.BaseHealth;
        model.BaseMana = hero.BaseMana;
        model.BaseArmor = hero.BaseArmor;
        model.BaseAttackMin = hero.BaseAttackMin;
        model.BaseAttackMax = hero.BaseAttackMax;
        model.BaseStrength = hero.BaseStrength;
        model.BaseAgility = hero.BaseAgility;
        model.BaseIntelligence = hero.BaseIntelligence;
        model.BaseHealthRegen = hero.BaseHealthRegen;
        model.BaseManaRegen = hero.BaseManaRegen;
        model.BaseMagicResistance = hero.BaseMagicResistance;
        model.CaptainsModeEnabled = hero.CaptainsModeEnabled;
        model.Legs = hero.Legs;
        model.StrengthGain = hero.StrengthGain;
        model.AgilityGain = hero.AgilityGain;
        model.IntelligenceGain = hero.IntelligenceGain;
        model.MovementSpeed = hero.MovementSpeed;
        model.TurnRate = hero.TurnRate;
        model.ProjectileSpeed = hero.ProjectileSpeed;
        model.Portrait = hero.GetPortraitVideoUrl();
        model.AttackRate = hero.AttackRate;
    }

    internal static void UpdateAbilities(this HeroModel model, _HeroAbilities heroAbility, IReadOnlyDictionary<string, _Ability> abilities)
    {
        model.Abilities = new();
        model.TalentTree = new();

        foreach (var abilityKey in heroAbility.Abilities.Where(x => !x.Contains("generic_hidden")))
        {
            if (abilities.ContainsKey(abilityKey))
            {
                var abilityModel = abilities[abilityKey].ToModel();
                abilityModel.Name = abilityKey;
                abilityModel.Demo = $"https://cdn.cloudflare.steamstatic.com/apps/dota2/videos/dota_react/abilities/{model.SteamCdnHeroName()}/{abilityKey}.webm";
                model.Abilities.Add(abilityModel);
            }
        }

        model.TalentTree = new TalentTree
        {
            Level10 = new(new TalentOption
            {
                Name = heroAbility.Talents[0].Name
            },
            new TalentOption
            {
                Name = heroAbility.Talents[1].Name
            }),
            Level15 = new(new TalentOption
            {
                Name = heroAbility.Talents[2].Name
            },
            new TalentOption
            {
                Name = heroAbility.Talents[3].Name
            }),
            Level20 = new(new TalentOption
            {
                Name = heroAbility.Talents[4].Name
            },
            new TalentOption
            {
                Name = heroAbility.Talents[5].Name
            }),
            Level25 = new(new TalentOption
            {
                Name = heroAbility.Talents[6].Name
            },
            new TalentOption
            {
                Name = heroAbility.Talents[7].Name
            }),
        };
    }

    public static AbilityModel ToModel(this _Ability ability)
    {

        return new AbilityModel
        {
            DisplayName = ability.Name,
            Behaviors = ability.Behaviors?.Where(x => x is not null).Select(x => ToBehavior(x)).ToList() ?? new(),
            DamageType = ability.DamageType?.Where(x => x is not null).Select(x => System.Enum.Parse<DamageType>(x)).ToList() ?? new(),
            IsBkbPiercing = ability.IsBkbPiercing,
            Description = ability.Description,
            Attributes = Filter(ability.Attributes),
            ManaCost = ability.ManaCost,
            Cooldown = ability.Cooldown,
            Lore = ability.Lore,
            IsDispellable = ability.IsDispellable,
            Image = $"{Heroes.SteamCDN}{ability.Image}",
            TargetTeam = ability.TargetTeam?.Where(x => x is not null).Select(x => System.Enum.Parse<TargetTeam>(x)).ToList() ?? new(),
            TargetType = ability.TargetType?.Where(x => x is not null).Select(x => System.Enum.Parse<TargetType>(x)).ToList() ?? new(),
            HasDemo = ability.HasDemo
        };
    }

    internal static AbilityAttribute[] Filter(AbilityAttribute[] attrs)
    {
        var list = attrs.ToList();
        list.RemoveAll(x => x.Header.Contains("ABILITYCOOLDOWN"));
        list.RemoveAll(x => x.Header.Contains("ABILITYMANACOST"));
        
        if(list.FirstOrDefault(x => x.Header.Contains("ABILITYCASTPOINT")) is { } a)
        {
            a.Header = a.Header.Replace("ABILITYCASTPOINT", "CAST POINT");
        }

        return list.ToArray();
    }

    internal static Behavior ToBehavior(this string behavior)
    {
        if(string.IsNullOrEmpty(behavior))
        {
            return Behavior.None;
        }

        if(behavior == "AOE")
        {
            return Behavior.Aoe;
        }
        else
        {
           return System.Enum.Parse<Behavior>(behavior.Replace(" ", string.Empty).Trim());
        }
    }
}
