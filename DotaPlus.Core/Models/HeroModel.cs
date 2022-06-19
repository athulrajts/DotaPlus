using System.Collections.Generic;

namespace DotaPlus.Core.Models
{
    public class HeroModel
    {
        public int? Id;
        public string Name { get; set; }
        public string LocalizedName { get; set; }
        public Attribute PrimaryAttribute { get; set; }
        public AttackType AttackType { get; set; }
        public List<Role> Roles { get; set; }
        public string Image { get; set; }
        public string Icon { get; set; }
        public float BaseHealth { get; set; }
        public float BaseHealthRegen { get; set; }
        public float BaseMana { get; set; }
        public float BaseManaRegen { get; set; }
        public float BaseArmor { get; set; }
        public float BaseMagicResistance { get; set; }
        public float BaseAttackMin { get; set; }
        public float BaseAttackMax { get; set; }
        public float BaseStrength { get; set; }
        public float BaseAgility { get; set; }
        public float BaseIntelligence { get; set; }
        public float StrengthGain { get; set; }
        public float AgilityGain { get; set; }
        public float IntelligenceGain { get; set; }
        public float AttackRange { get; set; }
        public float ProjectileSpeed { get; set; }
        public float AttackRate { get; set; }
        public float MovementSpeed { get; set; }
        public float? TurnRate { get; set; }
        public bool CaptainsModeEnabled { get; set; }
        public int Legs { get; set; }


        public string Portrait { get; set; }
        public List<AbilityModel> Abilities { get; set; }
        public TalentTree TalentTree { get; set; }
        public string Lore { get; set; }
    }
}
