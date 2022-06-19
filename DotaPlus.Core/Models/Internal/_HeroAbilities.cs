using DotaPlus.Core.Models.Internal;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DotaPlus.Core.Models;

[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public class _HeroAbilities
{
    [JsonPropertyName("abilities")]
    public string[] Abilities { get; set; }

    [JsonPropertyName("talents")]
    public _Talents[] Talents { get; set; }
}

