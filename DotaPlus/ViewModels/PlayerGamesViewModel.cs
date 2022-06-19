using CommunityToolkit.Mvvm.ComponentModel;
using DotaPlus.Contracts.Services;
using DotaPlus.Contracts.ViewModels;
using DotaPlus.Core.Contracts.Services;
using DotaPlus.Core.Models.Internal;
using DotaPlus.Models;
using Humanizer;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using OpenDotaApi;
using OpenDotaApi.Api.Matches.Model;
using OpenDotaApi.Api.Players.Model.Matches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotaPlus.ViewModels;

public partial class PlayerGamesViewModel : ObservableObject, INavigationAware
{
    private readonly ILocalSettingsService _localSettingsService;
    private readonly IHeroDetailsService _heroDetailsService;
    private readonly IDotaConstants _dotaContants;
    private readonly OpenDota openDota = new();

    [ObservableProperty]
    private List<MatchModel> _matches;

    [ObservableProperty]
    private long _steamId;

    [ObservableProperty]
    private MatchModel _selectedMatch;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private List<object> _tabs;

    public PlayerParameters Params { get; set; } = new PlayerParameters
    {
        Limit = 20
    };


    public PlayerGamesViewModel(ILocalSettingsService localSettingsService, IHeroDetailsService dotaConstants, IDotaConstants dotaContants)
    {
        _localSettingsService = localSettingsService;
        _heroDetailsService = dotaConstants;
        _dotaContants = dotaContants;
    }

    async partial void OnSelectedMatchChanged(MatchModel value)
    {
        if (value.MatchId.HasValue == false || value.HasCachedData == true)
        {
            return;
        }

        IsLoading = true;

        var match = await openDota.Matches.GetMatchAsync(value.MatchId.Value);
        await UpdateMatchModel(match);

        IsLoading = false;
    }

    public void OnNavigatedFrom()
    {
    }

    public async void OnNavigatedTo(object parameter)
    {
        SteamId = await _localSettingsService.ReadSettingAsync<long>(nameof(SteamId));

        if (SteamId == 0) return;

        IsLoading = true;
        Matches = (await openDota.Players.GetMatchesAsync(SteamId, Params)).Select(x => CreateMatchModel(x)).ToList();
        SelectedMatch = Matches.FirstOrDefault();
    }

    public MatchModel CreateMatchModel(Matches match)
    {
        var isPlayerRadiant = match.PlayerSlot < 128;
        var isWin = isPlayerRadiant ? match.RadiantWin : !match.RadiantWin;

        return new MatchModel
        {
            MatchId = match.MatchId,
            PlayerStats = new()
            {
                IsWin = isWin,
                Kills = match.Kills ?? 0,
                Deaths = match.Deaths ?? 0,
                Assists = match.Assists ?? 0,
                Team = isPlayerRadiant ? Team.Radiant : Team.Dire,
                Hero = _heroDetailsService.Heroes.SingleOrDefault(x => x.Id == match.HeroId),
            }
        };
    }

    public async Task UpdateMatchModel(Match match)
    {
        await AddSummaryPage(match);
        AddGpmXpmPage(match);
        SelectedMatch.HasCachedData = true;
        SelectedMatch.NotifyModelChanged();
    }

    public async Task<PlayerModel> CreatePlayer(MatchPlayer player)
    {
        int kills = 0;
        int assists = 0;

        if (player.Kills.HasValue)
        {
            kills = (int)player.Kills.Value;
        }

        if (player.Assists.HasValue)
        {
            assists = (int)player.Assists.Value;
        }

        

        var playerModel = new PlayerModel
        {
            SteamId = player.AccountId ?? 0,
            HeroId = player.HeroId.Value,
            Deaths = player.Deaths ?? 0,
            Kills = kills,
            Assists = assists,
            Networth = player.TotalGold ?? 0,
            Level = player.Level ?? 1,
            Name = string.IsNullOrEmpty(player.Personaname) ? "Anonymous" : player.Personaname,
            HeroDamage = player.HeroDamage ?? 0,
            Healing = player.HeroHealing ?? 0,
            BuildingDamage = player.TowerDamage ?? 0,
            LastHits = player.LastHits ?? 0,
            Denies = player.Denies ?? 0,
            Items = GetItems(player),
            Xpm = player.XpPerMin ?? 0,
            Gpm = player.GoldPerMin ?? 0
        };

        if (playerModel.SteamId > 0)
        {
            var playerDetails = await openDota.Players.GetPlayerAsync(playerModel.SteamId.Value);
            playerModel.Image = playerDetails.Profile.Avatar?.ToString();
            playerModel.ProfileUri = playerDetails.Profile.ProfileUrl;
        }


        return playerModel;
    }

    private List<ItemModel> GetItems(MatchPlayer player)
    {
        var items = new List<ItemModel>();
        if (player.Item0 is long value0 && value0 > 0)
        {
            items.Add(_dotaContants.ItemsById[value0]);
        }
        if (player.Item1 is long value1 && value1 > 0)
        {
            items.Add(_dotaContants.ItemsById[value1]);
        }
        if (player.Item2 is long value2 && value2 > 0)
        {
            items.Add(_dotaContants.ItemsById[value2]);
        }
        if (player.Item3 is long value3 && value3 > 0)
        {
            items.Add(_dotaContants.ItemsById[value3]);
        }
        if (player.Item4 is long value4 && value4 > 0)
        {
            items.Add(_dotaContants.ItemsById[value4]);
        }
        if (player.Item5 is long value5 && value5 > 0)
        {
            items.Add(_dotaContants.ItemsById[value5]);
        }
        return items;
    }

    public async Task AddSummaryPage(Match match)
    {
        if (SelectedMatch.Pages.OfType<MatchSummaryModel>().Any())
        {
            return;
        }

        var radiantPlayers = match.Players.Where(x => x.PlayerSlot < 128);
        var direPlayers = match.Players.Where(x => x.PlayerSlot > 127);
        var summaryModel = new MatchSummaryModel
        {
            IsRadiantWin = match.RadiantWin ?? false
        };
        foreach (var player in radiantPlayers)
        {
            summaryModel.RadiantPlayers.Add(await CreatePlayer(player));
        }
        foreach (var player in direPlayers)
        {
            summaryModel.DirePlayers.Add(await CreatePlayer(player));
        }
        summaryModel.RadiantScore = match.RadiantScore ?? 0;
        summaryModel.DireScore = match.DireScore ?? 0;
        summaryModel.Duration = TimeSpan.FromSeconds(match.Duration ?? 0);
        if(match.StartTime is DateTime dt)
        {
            summaryModel.EndTime = dt + summaryModel.Duration;
        }

        SelectedMatch.Pages.Add(summaryModel);
    }

    public void AddGpmXpmPage(Match match)
    {
        if (SelectedMatch.Pages.OfType<GpmXpChartsModel>().Any())
        {
            return;
        }

        var goldValues = new List<ObservablePoint>();
        var xpValues = new List<ObservablePoint>();
        if (match.RadiantGoldAdvantage is { })
        {
            for (int i = 0; i < match.RadiantGoldAdvantage.Count; i++)
            {
                goldValues.Add(new(i, match.RadiantGoldAdvantage[i]));
            }
        }

        if (match.RadiantXpAdvantage is { })
        {
            for (int i = 0; i < match.RadiantXpAdvantage.Count; i++)
            {
                xpValues.Add(new(i, match.RadiantXpAdvantage[i]));
            }
        }

        var chartsModel = new GpmXpChartsModel
        {
            Graphs = new[]
            {
                new LineSeries<ObservablePoint>
                {
                    Values = goldValues,
                    LineSmoothness = 1,
                    GeometrySize = 0,
                    Name = "Gold",
                    TooltipLabelFormatter = p => p.PrimaryValue.ToMetric(null, 1),
                },
                new LineSeries<ObservablePoint>
                {
                    Values = xpValues,
                    LineSmoothness = 1,
                    GeometrySize = 0,
                    Name = "XP",
                    TooltipLabelFormatter = p => p.PrimaryValue.ToMetric(null, 1),
                }
            }
        };

        SelectedMatch.Pages.Add(chartsModel);
    }
}


