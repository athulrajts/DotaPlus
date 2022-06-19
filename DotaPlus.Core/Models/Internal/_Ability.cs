using DotaPlus.Core.Helpers;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DotaPlus.Core.Models.Internal;

[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public class _Ability
{
    [JsonPropertyName("dname")]
    public string Name { get; set; }

    [JsonPropertyName("behavior")]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    public List<string> Behaviors { get; set; }

    [JsonPropertyName("dmg_type")]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    public List<string> DamageType { get; set; }

    [JsonPropertyName("bkbpierce")]
    [JsonConverter(typeof(YesNoBooleanConverter))]
    public bool IsBkbPiercing { get; set; }

    [JsonPropertyName("desc")]
    public string Description { get; set; }

    [JsonPropertyName("attrib")]
    public AbilityAttribute[] Attributes { get; set; }

    [JsonPropertyName("mc")]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    public List<string> ManaCost { get; set; }

    [JsonPropertyName("cd")]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    public List<string> Cooldown { get; set; }

    [JsonPropertyName("lore")]
    public string Lore { get; set; }

    [JsonPropertyName("dispellable")]
    [JsonConverter(typeof(YesNoBooleanConverter))]
    public bool IsDispellable { get; set; }

    [JsonPropertyName("img")]
    public string Image { get; set; }

    [JsonPropertyName("target_team")]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    public List<string> TargetTeam { get; set; }

    [JsonPropertyName("target_type")]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    public List<string> TargetType { get; set; }

    [JsonIgnore]
    public bool HasDemo { get; set; } = true;
}

