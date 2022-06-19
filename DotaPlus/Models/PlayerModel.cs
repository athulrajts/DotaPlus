using DotaPlus.Core.Models.Internal;
using Humanizer;
using System;
using System.Collections.Generic;

namespace DotaPlus.Models;

public class PlayerModel
{
    public int PlayerSlot { get; set; }
    public int HeroId { get; set; }
    public long? SteamId { get; set; }
    public int Kills { get; set; }
    public int Deaths { get; set; }
    public int Assists { get; set; }
    public long Networth { get; set; }
    public int Gpm { get; set; }
    public int Xpm { get; set; }
    public int LastHits { get; set; }
    public int Denies { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public Uri ProfileUri { get; set; }
    public int Level { get; set; }
    public double HeroDamage { get; set; }
    public double Healing { get; set; }
    public double BuildingDamage { get; set; }
    public List<ItemModel> Items { get; set; }
    public bool IsPublic => SteamId > 0;

    public string NetworthString => (Networth * 1.0).ToMetric(null, 1);
    public string HeroDamageString => HeroDamage.ToMetric(null, 1);
    public string HealingString => Healing.ToMetric(null, 1);
    public string BuildingDamageString => BuildingDamage.ToMetric(null, 1);
}
