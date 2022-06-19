using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.UI;
using DotaPlus.Contracts.ViewModels;
using OpenDotaApi;
using OpenDotaApi.Api.HeroStats.Model;
using System.Linq;

namespace DotaPlus.ViewModels;

public partial class HeroStatsViewModel : ObservableObject, INavigationAware
{

    OpenDota dota = new OpenDota();

    [ObservableProperty]
    public AdvancedCollectionView _stats;


    public HeroStatsViewModel()
    {
    }

    public void OnNavigatedFrom()
    {

    }

    public async void OnNavigatedTo(object parameter)
    {
        Stats = new((await dota.HeroStats.GetHeroStatsAsync()).Select(x => new HeroStatsModel(x)).ToList());
    }
}

public class HeroStatsModel : HeroStats
{
    public HeroStatsModel(HeroStats stats)
    {
        Name = stats.Name;
        LocalizedName = stats.LocalizedName;
        ProPick = stats.ProPick;
        ProBan = stats.ProBan;
        ProWin = stats.ProWin;
        HeraldPicks = stats.HeraldPicks;
        HeraldWins = stats.HeraldWins;
        GuardianPicks = stats.GuardianPicks;
        GuardianWins = stats.GuardianWins;
        CrusaderPicks = stats.CrusaderPicks;
        CrusaderWins = stats.CrusaderWins;
        ArchonPicks = stats.ArchonPicks;
        ArchonWins = stats.ArchonWins;
        LegendPicks = stats.LegendPicks;
        LegendWins = stats.LegendWins;
        AncientPicks = stats.AncientPicks;
        AncientWins = stats.AncientWins;
        DivinePicks = stats.DivinePicks;
        DivineWins = stats.DivineWins;
        ImmortalPicks = stats.ImmortalPicks;
        ImmortalWins = stats.ImmortalWins;
    }

    public double ProWinrate => (double)ProWin / (double)ProPick * 100;
    public double HeraldWinrate => (double)HeraldWins / (double)HeraldPicks * 100;
    public double GuardianWinrate => (double)GuardianWins / (double)GuardianPicks * 100;
    public double CrusaderWinrate => (double)CrusaderWins / (double)CrusaderPicks * 100;
    public double ArchonWinrate => (double)ArchonWins / (double)ArchonPicks * 100;
    public double LegendWinrate => (double)LegendWins / (double)LegendPicks * 100;
    public double AncientWinrate => (double)AncientWins / (double)AncientPicks * 100;
    public double DivineWinrate => (double)DivineWins / (double)DivinePicks * 100;
    public double ImmortalWinrate => (double)ImmortalWins / (double)ImmortalPicks * 100;

}