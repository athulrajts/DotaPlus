using DotaPlus.Core.Contracts.Services;
using DotaPlus.Core.Helpers;
using DotaPlus.Core.Models.Internal;
using DotaPlus.Models;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Linq;

namespace DotaPlus.Helpers
{
    public class DotaResourceHelper
    {
        private static readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static readonly LocalSettingsOptions _options;
        private static readonly IDotaConstants _constants;
        static DotaResourceHelper()
        {
            _options = App.GetService<IOptions<LocalSettingsOptions>>().Value;
            _constants = App.GetService<IDotaConstants>();
        }


        private static string CdnName(string name) => name.Replace("npc_dota_hero_", "").Trim();

        public static string GetHeroPortrait(string hero)
        {
            var folder = Path.Combine(_localAppData, _options.ApplicationDataFolder, "Assets", "Portrait");
            var file = Path.Combine(folder, $"{hero}.png");
            if (!File.Exists(file))
            {
                Heroes.DownloadImageAsync(folder, hero, new Uri($"{Heroes.SteamCDN}/apps/dota2/images/dota_react/heroes/{CdnName(hero)}.png")).Wait();
            }

            return file;
        }

        public static string GetHeroPortriat(int heroId)
        {
            var hero = _constants.HeroesById[heroId];
            return GetHeroPortrait(hero.Name);
        }

        public static string GetAbilityImage(string ability)
        {
            var folder = Path.Combine(_localAppData, _options.ApplicationDataFolder, "Assets", "Abilities");
            var file = Path.Combine(folder, $"{ability}.png");
            if (!File.Exists(file))
            {
                Heroes.DownloadImageAsync(folder, ability, new Uri($"{Heroes.SteamCDN}/apps/dota2/images/dota_react/abilities/{ability}.png")).Wait();
            }

            return file;
        }

        public static string GetAnimatedHeroPortrait(string hero)
        {
            var folder = Path.Combine(_localAppData, _options.ApplicationDataFolder, "Assets", "AnimatedPortrait");
            var file = Path.Combine(folder, $"{hero}.webm");
            if (!File.Exists(file))
            {
                Heroes.DownloadImageAsync(folder, hero, new Uri($"{Heroes.SteamCDNVideo}{CdnName(hero)}.webm")).Wait();
            }

            return file;
        }

        public static string GetAbilityDemo(string ability)
        {
            var folder = Path.Combine(_localAppData, _options.ApplicationDataFolder, "Assets", "AbilityDemo");
            var file = Path.Combine(folder, $"{ability}.webm");
            if (!File.Exists(file))
            {
                Heroes.DownloadImageAsync(folder, ability, new Uri($"{Heroes.SteamCDNVideo}{CdnName(ability)}.webm")).Wait();
            }

            return file;
        }

        public static string GetItemImage(long itemId)
        {
            var item = _constants.ItemsById[itemId];
            return GetItemImage(item);
        }

        public static string GetItemImage(ItemModel item)
        {
            var folder = Path.Combine(_localAppData, _options.ApplicationDataFolder, "Assets", "Items");
            var file = Path.Combine(folder, $"{item.Name}.png");
            if (!File.Exists(file))
            {
                Heroes.DownloadImageAsync(folder, item.Name, new Uri($"{Heroes.SteamCDN}{item.Image}")).Wait();
            }
            return file;
        }
    }
}
