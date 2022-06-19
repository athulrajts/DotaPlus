using System.Collections.Generic;

namespace DotaPlus.Core.Models
{
    public class AbilityModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public List<Behavior> Behaviors { get; set; }
        public List<DamageType> DamageType { get; set; }
        public bool IsBkbPiercing { get; set; }
        public string Description { get; set; }
        public AbilityAttribute[] Attributes { get; set; }
        public List<string> ManaCost { get; set; }
        public List<string> Cooldown { get; set; }
        public string Lore { get; set; }
        public bool IsDispellable { get; set; }
        public string Image { get; set; }
        public List<TargetTeam> TargetTeam { get; set; }
        public List<TargetType> TargetType { get; set; }
        public string Demo { get; set; }
        public bool HasDemo { get; set; }

        public string GetBehaviorString()
        {
            return string.Join(" | ", Behaviors);
        }

        public string GetDamageTypeString()
        {
            return string.Join(" | ", DamageType);
        }

        public string GetCooldownString() => Cooldown is null ? string.Empty : string.Join("/", Cooldown);
        public string GetManaCostString() => ManaCost is null ? string.Empty : string.Join("/", ManaCost);
    }
}
