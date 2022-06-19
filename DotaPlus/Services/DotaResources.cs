using DotaPlus.Core.Contracts.Services;
using DotaPlus.Core.Helpers;
using DotaPlus.Core.Models;
using DotaPlus.Core.Models.Internal;
using DotaPlus.Models;
using Microsoft.Extensions.Options;
using OpenDotaApi;
using OpenDotaApi.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotaPlus.Services;

public class DotaResources : IDotaConstants
{
    private readonly OpenDota _od = new();
    private readonly LocalSettingsOptions _options;
    private static readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    private readonly string snapshot;
    private Dictionary<string, List<string>> invalids = EmptyInvalids();

    public Dictionary<int, _Hero> ById { get; set; }
    public Dictionary<string, _Hero> ByName { get; set; } = new Dictionary<string, _Hero>();
    public Dictionary<string, string> HeroLore { get; set; }
    public Dictionary<string, _HeroAbilities> HeroAbilities { get; set; }
    public Dictionary<string, _Ability> Abilities { get; set; }
    public Dictionary<string, ItemModel> Items { get; set; }
    public Dictionary<long, ItemModel> ItemsById { get; set; } = new();

    IReadOnlyDictionary<int, _Hero> IDotaConstants.HeroesById => ById;
    IReadOnlyDictionary<string, _Hero> IDotaConstants.HeroesByName => ByName;
    IReadOnlyDictionary<string, string> IDotaConstants.HeroLore => HeroLore;
    IReadOnlyDictionary<string, _HeroAbilities> IDotaConstants.HeroAbilities => HeroAbilities;
    IReadOnlyDictionary<string, _Ability> IDotaConstants.Abilities => Abilities;
    IReadOnlyDictionary<string, ItemModel> IDotaConstants.Items => Items;
    IReadOnlyDictionary<long, ItemModel> IDotaConstants.ItemsById => ItemsById;


    private readonly string Constants;
    private readonly string Portraits;
    private readonly string abilities;
    private readonly string AnimatedPortrait;
    private readonly string ObsoletesFile;
    private readonly string AbilityDemo;
    private readonly string Item;
    const string HERO = "hero";
    const string HERO_ANIMATED = "hero-animated";
    const string ABILITY = "ability";
    const string ABILITY_DEMO = "ability-video";
    const string ITEM = "item";

    static Dictionary<string, List<string>> EmptyInvalids() => new()
    {
        ["hero"] = new(),
        ["hero-animated"] = new(),
        ["ability"] = new(),
        ["ability-video"] = new(),
        ["item"] = new()
    };

    public DotaResources(IOptions<LocalSettingsOptions> options)
    {
        _options = options.Value;
        Constants = Path.Combine(_localAppData, _options.ApplicationDataFolder, "Constants");
        Portraits = Path.Combine(_localAppData, _options.ApplicationDataFolder, "Assets", "Portrait");
        AnimatedPortrait = Path.Combine(_localAppData, _options.ApplicationDataFolder, "Assets", "AnimatedPortrait");
        abilities = Path.Combine(_localAppData, _options.ApplicationDataFolder, "Assets", "Abilities");
        ObsoletesFile = Path.Combine(_localAppData, _options.ApplicationDataFolder, "Assets", "obsolete.json");
        AbilityDemo = Path.Combine(_localAppData, _options.ApplicationDataFolder, "Assets", "AbilityDemo");
        Item = Path.Combine(_localAppData, _options.ApplicationDataFolder, "Assets", "Items");


        if (File.Exists(ObsoletesFile))
        {
            snapshot = File.ReadAllText(ObsoletesFile);
            invalids = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(snapshot) ?? EmptyInvalids();
        }

        Directory.CreateDirectory(Constants);
        Directory.CreateDirectory(Portraits);
        Directory.CreateDirectory(abilities);
        Directory.CreateDirectory(AnimatedPortrait);
        Directory.CreateDirectory(AbilityDemo);
        Directory.CreateDirectory(Item);
    }

    public async Task Initialize()
    {
        await EnsureHeroes();
        await EnsureAbilities();
        await EnsureItems();
        //await EnsureHeroPortraits();
        //await EnsureAnimatedHeroPortraits();
        //await EnsureAbilityDemos();
        //await EnsureAbilityImages();
        //await EnsureItemImages();

        var current = JsonSerializer.Serialize(invalids);
        if(current != snapshot)
        {
            File.WriteAllText(ObsoletesFile, current);
        }

    }

    private async Task EnsureItems() 
    {
        await EnsureConstant<string, ItemModel>(EnumConstants.Items, c => Items = c);

        foreach (var item in Items)
        {
            item.Value.Name = item.Key;
            ItemsById.Add(item.Value.Id, item.Value);
        }
    }
    private async Task EnsureAbilities() => await EnsureConstant<string, _Ability>(EnumConstants.Abilities, c => Abilities = c);
    private async Task EnsureHeroes()
    {
        await EnsureConstant<int, _Hero>(EnumConstants.Heroes, c => ById = c);
        await EnsureConstant<string, string>(EnumConstants.HeroLore, c => HeroLore = c);
        await EnsureConstant<string, _HeroAbilities>(EnumConstants.HeroAbilities, c => HeroAbilities = c);

        foreach (var heroModel in ById.Values)
        {
            ByName.Add(heroModel.Name, heroModel);
        }
    }

    private async Task EnsureConstant<TKey, TValue>(EnumConstants constType, Action<Dictionary<TKey, TValue>> update)
    {
        var file = GetConstantsPath(constType);
        string json = string.Empty;

        if (File.Exists(file))
        {
            json = File.ReadAllText(file);
        }
        else
        {
            json = await _od.Constants.GetGameConstantsAsync(constType);
            File.WriteAllText(file, json);
        }

        var model = JsonSerializer.Deserialize<Dictionary<TKey, TValue>>(json);
        update(model);
    }

    private string GetConstantsPath(EnumConstants constType)
    {
        return Path.Combine(Constants, $"{constType}.json");
    }

    private async Task EnsureAbilityDemos()
    {
        foreach (var item in HeroAbilities)
        {
            var hero = ByName[item.Key];
            foreach (var ability in item.Value.Abilities.Where(x => x != "generic_hidden"))
            {
                var videofile = $"{ability}.webm";
                var fullPath = Path.Combine(AbilityDemo, videofile);

                if (invalids[ABILITY_DEMO].Contains(ability))
                {
                    Abilities[ability].HasDemo = false;
                    continue;
                }

                if (!File.Exists(fullPath))
                {
                    try
                    {

                        await Heroes.DownloadImageAsync(AbilityDemo, ability, new Uri($"{Heroes.SteamCDN}/apps/dota2/videos/dota_react/abilities/{hero.SteamCdnHeroName()}/{ability}.webm"));
                    }
                    catch (HttpRequestException ex)
                    {
                        if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            invalids[ABILITY_DEMO].Add(ability);
                            Abilities[ability].HasDemo = false;
                        }
                    }
                }
            }
        }
    }

    private async Task EnsureAbilityImages()
    {
        foreach (var item in HeroAbilities)
        {
            var hero = ByName[item.Key];
            foreach (var ability in item.Value.Abilities.Where(x => x != "generic_hidden"))
            {
                var imagefile = $"{ability}.png";
                var fullPath = Path.Combine(abilities, imagefile);

                if(invalids[ABILITY].Contains(ability))
                {
                    continue;
                }

                if (!File.Exists(fullPath))
                {
                    try
                    {
                        await Heroes.DownloadImageAsync(abilities, ability, new Uri($"{Heroes.SteamCDN}/apps/dota2/images/dota_react/abilities/{ability}.png"));
                    }
                    catch (HttpRequestException ex)
                    {
                        if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            invalids[ABILITY].Add(ability);
                        }
                    }
                }
            }
        }
    }

    private async Task EnsureAnimatedHeroPortraits()
    {
        foreach (var hero in ByName.Values)
        {
            var animated = Path.Combine(AnimatedPortrait, $"{hero.Name}.webm");

            if(invalids[HERO_ANIMATED].Contains(hero.Name))
            {
                continue;
            }

            if (!File.Exists(animated))
            {
                try
                {
                    await Heroes.DownloadImageAsync(AnimatedPortrait, hero.Name, new Uri(hero.GetPortraitVideoUrl()));
                }
                catch (HttpRequestException ex)
                {
                    if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        invalids[HERO_ANIMATED].Add(hero.Name);
                    }
                }
            }
        }
    }

    private async Task EnsureHeroPortraits()
    {
        foreach (var hero in ByName.Values)
        {
            var image = Path.Combine(Portraits, $"{hero.Name}.png");

            if (invalids[HERO].Contains(hero.Name))
            {
                continue;
            }

            if (!File.Exists(image))
            {
                try
                {
                    await Heroes.DownloadImageAsync(Portraits, hero.Name, new Uri(hero.GetImageUrl()));
                }
                catch (HttpRequestException ex)
                {
                    if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        invalids[HERO].Add(hero.Name);
                    }
                }
            }
        }
    }

    private async Task EnsureItemImages()
    {
        foreach (var item in Items)
        {
            var image = Path.Combine(Item, $"{item.Key}.png");
            
            if (invalids[ITEM].Contains(item.Key))
            {
                continue;
            }

            if (!File.Exists(image))
            {
                try
                {
                    await Heroes.DownloadImageAsync(Item, item.Key, new Uri($"{Heroes.SteamCDN}{item.Value.Image}"));
                }
                catch (HttpRequestException ex)
                {
                    if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        invalids[ITEM].Add(item.Key);
                    }
                }
            }
        }
    }

}
