using System.Text.Json.Serialization;

namespace DotaPlus.Core.Models.Internal;

public class ItemModel
{
    [JsonPropertyName("hint")]
    public string[] Hint { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("img")]
    public string Image { get; set; }

    [JsonPropertyName("dname")]
    public string DisplayName { get; set; }

    [JsonPropertyName("qual")]
    public string Qual { get; set; }

    [JsonPropertyName("cost")]
    public int? Cost { get; set; }

    [JsonPropertyName("notes")]
    public string Notes { get; set; }

    [JsonPropertyName("attrib")]
    public ItemAttribute[] Attributes { get; set; }

    [JsonPropertyName("mc")]
    public object ManaCost { get; set; }

    [JsonPropertyName("cd")]
    public object Cooldown { get; set; } // will be false if not cool down, otherwise will contain cd.

    [JsonPropertyName("lore")]
    public string Lore { get; set; }

    [JsonPropertyName("components")]
    public string[] Components { get; set; }

    [JsonPropertyName("created")]
    public bool IsCreated { get; set; }

    [JsonPropertyName("charges")]
    public object Charges { get; set; }// will be false if no charges, otherwise will contain charges.

    [JsonIgnore]
    public string Name { get; set; }

}

public class ItemAttribute
{
    [JsonPropertyName("key")]
    public string Key { get; set; }

    [JsonPropertyName("header")]
    public string Header { get; set; }

    [JsonPropertyName("value")]
    public object Value { get; set; }

    [JsonPropertyName("footer")]
    public string Footer { get; set; }
}
