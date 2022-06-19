using DotaPlus.Core.Helpers;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DotaPlus.Core.Models;

public class AbilityAttribute
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("header")]
    public string Header { get; set; }

    [JsonPropertyName("value")]
    [JsonConverter(typeof(SingleOrArrayConverter<string>))]
    public List<string> Values { get; set; }

    [JsonPropertyName("generated")]
    public bool? Generated { get; set; }

    public override string ToString()
    {
        return string.Join("/", Values);
    }
}

