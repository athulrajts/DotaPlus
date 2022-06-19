using DotaPlus.Core.Models;

namespace DotaPlus.Models;

public class PlayerStats
{
    public bool? IsWin { get; set; }
    public Team Team { get; set; }
    public HeroModel Hero { get; set; }
    public int Kills { get; set; }
    public int Deaths { get; set; }
    public int Assists { get; set; }
    public string Kda => $@"{Kills}/{Deaths}/{Assists}";
}
