using System.ComponentModel;
using System.Text.Json.Serialization;

namespace DotaPlus.Core.Models.Internal;

[Browsable(false)]
[EditorBrowsable(EditorBrowsableState.Never)]
public class _Talents
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("level")]
    public int Level { get; set; }
}

