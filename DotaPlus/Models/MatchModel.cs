using CommunityToolkit.Mvvm.ComponentModel;
using Humanizer;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotaPlus.Models;

public class MatchModel : ObservableObject
{
    public long? MatchId { get; set; }
    public PlayerStats PlayerStats { get; set; }
    public List<IPivotTab> Pages { get; set; } = new();
    public IEnumerable<IPivotTab> VisiblePages => Pages.Where(x => x.IsVisible);
    public bool HasCachedData { get; set; }
    public void NotifyModelChanged() => OnPropertyChanged(string.Empty); 
}

public interface IPivotTab
{
    bool IsVisible { get; }
    string Header { get; }
}

public class MatchSummaryModel : IPivotTab
{
    public bool IsRadiantWin { get; set; }
    public List<PlayerModel> RadiantPlayers { get; set; } = new();
    public List<PlayerModel> DirePlayers { get; set; } = new();
    public int RadiantScore { get; set; }
    public int DireScore { get; set; }
    public string Header { get; } = "General";
    public TimeSpan Duration { get; set; }
    public DateTime EndTime { get; set; }

    public string VictoryString => string.Format("{0} Victory", IsRadiantWin ? "Radiant": "Dire");
    public string DurationString => Duration.Humanize();
    public string EndTimeString => EndTime.Humanize();
    public bool IsVisible => true;
    public override string ToString() => Header;
}

public class GpmXpChartsModel : IPivotTab
{
    public ISeries[] Graphs { get; set; }
    public ICartesianAxis[] XAxes { get; }
    public ICartesianAxis[] YAxes { get; }
    public bool IsVisible => Graphs[0].Values.GetEnumerator().MoveNext() || Graphs[1].Values.GetEnumerator().MoveNext();
    public string Header { get; } = "Gold/Xp";

    public GpmXpChartsModel()
    {
        XAxes = new Axis[]
        {
            new Axis
            {
                Name = "Time",
            },
        };
        YAxes = new Axis[]
        {
            new Axis
            {
                Name = "Gold/XP",
            },
        };
    }

    public override string ToString() => Header;
}
