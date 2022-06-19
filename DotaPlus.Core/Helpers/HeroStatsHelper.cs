using DotaPlus.Core.Models;
using System;
using Attribute = DotaPlus.Core.Models.Attribute;

namespace DotaPlus.Core.Helpers
{
    public static class HeroStatsHelper
    {
        public const double STRENGTH_TO_HEALTH_FACTOR = 20;
        public const double STRENGTH_TO_REGEN_FACTOR = 0.1;
        public const double INTELLIGENCE_TO_MANA_FACTOR = 12;
        public const double INTELLIGENCE_TO_MANA_REGEN_FACTOR = 0.05;

        public static double GetHealth(this HeroModel model, int level = 1)
        {
            var baseHealth = model.BaseHealth;
            var healthGain = (level - 1) * model.StrengthGain * STRENGTH_TO_HEALTH_FACTOR;
            var baseStrengthGain = model.BaseStrength * STRENGTH_TO_HEALTH_FACTOR;
            return Math.Round(baseHealth + healthGain + baseStrengthGain, 1);
        }

        public static string GetHealthString(HeroModel model, int level = 1) => Math.Round(GetHealth(model, level), 1).ToString("0.##");

        public static double GetHealthRegen(this HeroModel model, int level = 1)
        {
            var baseHealthRegen = model.BaseHealthRegen;
            var healthRegenGain = (level - 1) * model.StrengthGain * STRENGTH_TO_REGEN_FACTOR;
            var baseStrengthRegenGain = model.BaseStrength * STRENGTH_TO_REGEN_FACTOR;
            return Math.Round(baseHealthRegen + healthRegenGain + baseStrengthRegenGain, 1);
        }

        public static string GetHealthRegenString(this HeroModel model, int level = 1) => Math.Round(GetHealthRegen(model, level),1).ToString("0.##");

        public static double GetMana(this HeroModel model, int level = 1)
        {
            var baseMana = model.BaseMana;
            var manaGain = (level - 1) * model.IntelligenceGain * INTELLIGENCE_TO_MANA_FACTOR;
            var baseIntGain = model.BaseIntelligence * INTELLIGENCE_TO_MANA_FACTOR;
            return Math.Round(baseMana + manaGain + baseIntGain, 1);
        }

        public static string GetManaString(this HeroModel model, int level = 1) => Math.Round(GetMana(model, level),1).ToString("0.##");

        public static double GetManaRegen(this HeroModel model, int level = 1)
        {
            var baseManaRegen = model.BaseManaRegen;
            var manaRegenGain = (level - 1) * model.IntelligenceGain * INTELLIGENCE_TO_MANA_REGEN_FACTOR;
            var baseIntRegenGain = model.BaseIntelligence * INTELLIGENCE_TO_MANA_REGEN_FACTOR;
            return Math.Round(baseManaRegen + manaRegenGain + baseIntRegenGain, 1);
        }

        public static string GetManaRegenString(this HeroModel model, int level = 1) => Math.Round(GetManaRegen(model, level), 1).ToString("0.##");

        public static double GetBaseDamageMax(this HeroModel model)
        {
            var baseDamage = model.BaseAttackMax;
            var attributeDamage = model.PrimaryAttribute switch
            {
                Attribute.Agility => model.BaseAgility,
                Attribute.Intelligence => model.BaseIntelligence,
                Attribute.Strength => model.BaseStrength,
                _ => throw new NotImplementedException()
            };

            return baseDamage + attributeDamage;
        }

        public static double GetBaseDamageMin(this HeroModel model)
        {
            var baseDamage = model.BaseAttackMin;
            var attributeDamage = model.PrimaryAttribute switch
            {
                Attribute.Agility => model.BaseAgility,
                Attribute.Intelligence => model.BaseIntelligence,
                Attribute.Strength => model.BaseStrength,
                _ => throw new NotImplementedException()
            };

            return baseDamage + attributeDamage;
        }

        public static string GetAttackDamageString(this HeroModel model) => $"{model.GetBaseDamageMin() - model.GetBaseDamageMax()}";

        public static string GetWinRate(int? win, int? pick)
        {
            double? winrate = (win / (double)pick * 100);
            return winrate.HasValue ? winrate.Value.ToString("0.##") : "0";
        }
    }
}
