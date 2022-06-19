using DotaPlus.Core.Models;
using DotaPlus.Core.Models.Internal;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DotaPlus.Core.Helpers;

public static class Heroes
{
    public const string Abaddon = "npc_dota_hero_abaddon";
    public const string Alchemist = "npc_dota_hero_alchemist";
    public const string AncientApparition = "npc_dota_hero_ancient_apparition";
    public const string AntiMage = "npc_dota_hero_antimage";
    public const string ArcWarden = "npc_dota_hero_arc_warden";
    public const string Axe = "npc_dota_hero_axe";
    public const string Bane = "npc_dota_hero_bane";
    public const string Batrider = "npc_dota_hero_batrider";
    public const string Beastmaster = "npc_dota_hero_beastmaster";
    public const string Bloodseeker = "npc_dota_hero_bloodseeker";
    public const string BountyHunter = "npc_dota_hero_bounty_hunter";
    public const string Brewmaster = "npc_dota_hero_brewmaster";
    public const string Bristleback = "npc_dota_hero_bristleback";
    public const string Broodmother = "npc_dota_hero_broodmother";
    public const string CentaurWarrunner = "npc_dota_hero_centaur";
    public const string ChaosKnight = "npc_dota_hero_chaos_knight";
    public const string Chen = "npc_dota_hero_chen";
    public const string Clinkz = "npc_dota_hero_clinkz";
    public const string Clockwerk = "npc_dota_hero_rattletrap";
    public const string CrystalMaiden = "npc_dota_hero_crystal_maiden";
    public const string DarkSeer = "npc_dota_hero_dark_seer";
    public const string DarkWillow = "npc_dota_hero_dark_willow";
    public const string Dawnbreaker = "npc_dota_hero_dawnbreaker";
    public const string Dazzle = "npc_dota_hero_dazzle";
    public const string DeathProphet = "npc_dota_hero_death_prophet";
    public const string Disruptor = "npc_dota_hero_disruptor";
    public const string Doom = "npc_dota_hero_doom_bringer";
    public const string DragonKnight = "npc_dota_hero_dragon_knight";
    public const string DrowRanger = "npc_dota_hero_drow_ranger";
    public const string EarthSpirit = "npc_dota_hero_earth_spirit";
    public const string Earthshaker = "npc_dota_hero_earthshaker";
    public const string ElderTitan = "npc_dota_hero_elder_titan";
    public const string EmberSpirit = "npc_dota_hero_ember_spirit";
    public const string Enchantress = "npc_dota_hero_enchantress";
    public const string Enigma = "npc_dota_hero_enigma";
    public const string FacelessVoid = "npc_dota_hero_faceless_void";
    public const string Grimstroke = "npc_dota_hero_grimstroke";
    public const string Gyrocopter = "npc_dota_hero_gyrocopter";
    public const string Hoodwink = "npc_dota_hero_hoodwink";
    public const string Huskar = "npc_dota_hero_huskar";
    public const string Invoker = "npc_dota_hero_invoker";
    public const string Io = "npc_dota_hero_wisp";
    public const string Jakiro = "npc_dota_hero_jakiro";
    public const string Juggernaut = "npc_dota_hero_juggernaut";
    public const string KeeperoftheLight = "npc_dota_hero_keeper_of_the_light";
    public const string Kunkka = "npc_dota_hero_kunkka";
    public const string LegionCommander = "npc_dota_hero_legion_commander";
    public const string Leshrac = "npc_dota_hero_leshrac";
    public const string Lich = "npc_dota_hero_lich";
    public const string Lifestealer = "npc_dota_hero_life_stealer";
    public const string Lina = "npc_dota_hero_lina";
    public const string Lion = "npc_dota_hero_lion";
    public const string LoneDruid = "npc_dota_hero_lone_druid";
    public const string Luna = "npc_dota_hero_luna";
    public const string Lycan = "npc_dota_hero_lycan";
    public const string Magnus = "npc_dota_hero_magnataur";
    public const string Marci = "npc_dota_hero_marci";
    public const string Mars = "npc_dota_hero_mars";
    public const string Medusa = "npc_dota_hero_medusa";
    public const string Meepo = "npc_dota_hero_meepo";
    public const string Mirana = "npc_dota_hero_mirana";
    public const string MonkeyKing = "npc_dota_hero_monkey_king";
    public const string Morphling = "npc_dota_hero_morphling";
    public const string NagaSiren = "npc_dota_hero_naga_siren";
    public const string NaturesProphet = "npc_dota_hero_furion";
    public const string Necrophos = "npc_dota_hero_necrolyte";
    public const string NightStalker = "npc_dota_hero_night_stalker";
    public const string NyxAssassin = "npc_dota_hero_nyx_assassin";
    public const string OgreMagi = "npc_dota_hero_ogre_magi";
    public const string Omniknight = "npc_dota_hero_omniknight";
    public const string Oracle = "npc_dota_hero_oracle";
    public const string OutworldDevourer = "npc_dota_hero_obsidian_destroyer";
    public const string Pangolier = "npc_dota_hero_pangolier";
    public const string PhantomAssassin = "npc_dota_hero_phantom_assassin";
    public const string PhantomLancer = "npc_dota_hero_phantom_lancer";
    public const string Phoenix = "npc_dota_hero_phoenix";
    public const string Puck = "npc_dota_hero_puck";
    public const string Pudge = "npc_dota_hero_pudge";
    public const string Pugna = "npc_dota_hero_pugna";
    public const string QueenofPain = "npc_dota_hero_queenofpain";
    public const string Razor = "npc_dota_hero_razor";
    public const string Riki = "npc_dota_hero_riki";
    public const string Rubick = "npc_dota_hero_rubick";
    public const string SandKing = "npc_dota_hero_sand_king";
    public const string ShadowDemon = "npc_dota_hero_shadow_demon";
    public const string ShadowFiend = "npc_dota_hero_nevermore";
    public const string ShadowShaman = "npc_dota_hero_shadow_shaman";
    public const string Silencer = "npc_dota_hero_silencer";
    public const string SkywrathMage = "npc_dota_hero_skywrath_mage";
    public const string Slardar = "npc_dota_hero_slardar";
    public const string Slark = "npc_dota_hero_slark";
    public const string Snapfire = "npc_dota_hero_snapfire";
    public const string Sniper = "npc_dota_hero_sniper";
    public const string Spectre = "npc_dota_hero_spectre";
    public const string SpiritBreaker = "npc_dota_hero_spirit_breaker";
    public const string StormSpirit = "npc_dota_hero_storm_spirit";
    public const string Sven = "npc_dota_hero_sven";
    public const string Techies = "npc_dota_hero_techies";
    public const string TemplarAssassin = "npc_dota_hero_templar_assassin";
    public const string Terrorblade = "npc_dota_hero_terrorblade";
    public const string Tidehunter = "npc_dota_hero_tidehunter";
    public const string Timbersaw = "npc_dota_hero_shredder";
    public const string Tinker = "npc_dota_hero_tinker";
    public const string Tiny = "npc_dota_hero_tiny";
    public const string TreantProtector = "npc_dota_hero_treant";
    public const string TrollWarlord = "npc_dota_hero_troll_warlord";
    public const string Tusk = "npc_dota_hero_tusk";
    public const string Underlord = "npc_dota_hero_abyssal_underlord";
    public const string Undying = "npc_dota_hero_undying";
    public const string Ursa = "npc_dota_hero_ursa";
    public const string VengefulSpirit = "npc_dota_hero_vengefulspirit";
    public const string Venomancer = "npc_dota_hero_venomancer";
    public const string Viper = "npc_dota_hero_viper";
    public const string Visage = "npc_dota_hero_visage";
    public const string VoidSpirit = "npc_dota_hero_void_spirit";
    public const string Warlock = "npc_dota_hero_warlock";
    public const string Weaver = "npc_dota_hero_weaver";
    public const string Windranger = "npc_dota_hero_windrunner";
    public const string WinterWyvern = "npc_dota_hero_winter_wyvern";
    public const string WitchDoctor = "npc_dota_hero_witch_doctor";
    public const string WraithKing = "npc_dota_hero_skeleton_king";
    public const string Zeus = "npc_dota_hero_zuus";
    public const string SteamCDN = "https://cdn.cloudflare.steamstatic.com";
    public const string SteamCDNVideo = "https://cdn.cloudflare.steamstatic.com/apps/dota2/videos/dota_react/heroes/renders/";



    public static string FromAlias(string alias)
    {
        return alias switch
        {
            "aba" or "abaddon" => Abaddon,
            "alch" or "alchemist" => Alchemist,
            "aa" or "ancientapparition" => AncientApparition,
            "am" or "antimage" => AntiMage,
            "zet" or "arcwarden" => ArcWarden,
            "axe" => Axe,
            "bane" => Bane,
            "bat" or "batrider" => Batrider,
            "bm" or "beast" or "beastmaster" => Beastmaster,
            "bs" or "seeker" or "bloodseeker" => Bloodseeker,
            "bh" or "bounty" or "bountyhunter" => BountyHunter,
            "brew" or "brewmaster" => Brewmaster,
            "bb" or "bristle" or "bristleback" => Bristleback,
            "brood" or "broodmother" => Broodmother,
            "centaur" or "centaurwarrunner" => CentaurWarrunner,
            "ck" or "chaosknight" => ChaosKnight,
            "chen" => Chen,
            "clinkz" => Clinkz,
            "clock" or "clockwerk" => Clockwerk,
            "cm" or "crystalmaiden" => CrystalMaiden,
            "ds" or "darkseer" => DarkSeer,
            "dw" or "willow" or "darkwillow" => DarkWillow,
            "dawn" or "dawnbreaker" => Dawnbreaker,
            "dazzle" => Dazzle,
            "dp" or "deathprophet" => DeathProphet,
            "disruptor" => Disruptor,
            "doom" => Doom,
            "dk" or "dragonknight" => DragonKnight,
            "drow" or "trax" or "drowranger" => DrowRanger,
            "espirit" or "earthspirit" => EarthSpirit,
            "eshaker" or "earthshaker" => Earthshaker,
            "eldertitan" => ElderTitan,
            "ember" or "emberspirit" => EmberSpirit,
            "ench" or "enchantress" => Enchantress,
            "enigma" => Enigma,
            "void" or "fv" or "facelessvoid" => FacelessVoid,
            "grim" or "grimstroke" => Grimstroke,
            "gyro" or "gyrocopter" => Gyrocopter,
            "hoodwink" => Hoodwink,
            "huskar" => Huskar,
            "invo" or "invoker" => Invoker,
            "io" => Io,
            "jakiro" => Jakiro,
            "jug" or "juggernaut" => Juggernaut,
            "kotl" or "keeperofthelight" => KeeperoftheLight,
            "kunkka" => Kunkka,
            "lc" or "legioncommander" => LegionCommander,
            "lesh" or "leshrac" => Leshrac,
            "lich" => Lich,
            "ls" or "lifestealer" => Lifestealer,
            "lina" => Lina,
            "lion" => Lion,
            "druid" or "lonedruid" => LoneDruid,
            "luna" => Luna,
            "lycan" => Lycan,
            "mag" or "magnus" => Magnus,
            "marci" => Marci,
            "mars" => Mars,
            "dusa" or "medusa" => Medusa,
            "meepo" => Meepo,
            "mirana" => Mirana,
            "mk" or "monkeyking" => MonkeyKing,
            "morph" or "morphling" => Morphling,
            "naga" or "nagasiren" => NagaSiren,
            "np" or "naturesprophet" => NaturesProphet,
            "necro" or "necrophos" => Necrophos,
            "ns" or "nightstalker" => NightStalker,
            "nyx" or "nyxassassin" => NyxAssassin,
            "ogre" or "ogremagi" => OgreMagi,
            "omni" or "omniknight" => Omniknight,
            "oracle" => Oracle,
            "od" or "outworlddevourer" => OutworldDevourer,
            "pango" or "pangolier" => Pangolier,
            "pa" or "phantomassassin" => PhantomAssassin,
            "pl" or "phantomlancer" => PhantomLancer,
            "phoenix" => Phoenix,
            "puck" => Puck,
            "pudge" => Pudge,
            "pugna" => Pugna,
            "qop" or "queenofpain" => QueenofPain,
            "razor" => Razor,
            "riki" => Riki,
            "rubick" => Rubick,
            "sk" or "sandking" => SandKing,
            "sd" or "shadowdemon" => ShadowDemon,
            "sf" or "shadowfiend" => ShadowFiend,
            "shaman" or "shadowshaman" => ShadowShaman,
            "silencer" => Silencer,
            "sky" or "skywrathmage" => SkywrathMage,
            "slardar" => Slardar,
            "slark" => Slark,
            "snapfire" => Snapfire,
            "sniper" => Sniper,
            "spectre" => Spectre,
            "bara" or "sb" or "spiritbreaker" => SpiritBreaker,
            "storm" or "stormspirit" => StormSpirit,
            "sven" => Sven,
            "techies" => Techies,
            "ta" or "templarassassin" => TemplarAssassin,
            "tb" or "terrorblade" => Terrorblade,
            "tide" or "tidehunter" => Tidehunter,
            "timber" or "timbersaw" => Timbersaw,
            "tinker" => Tinker,
            "tiny" => Tiny,
            "treant" or "treantprotector" => TreantProtector,
            "troll" or "trollwarlord" => TrollWarlord,
            "tusk" => Tusk,
            "underlord" => Underlord,
            "undying" => Undying,
            "ursa" => Ursa,
            "venge" or "vengefulspirit" => VengefulSpirit,
            "veno" or "venomancer" => Venomancer,
            "viper" => Viper,
            "visage" => Visage,
            "vspirit" or "voidspirit" => VoidSpirit,
            "warlock" => Warlock,
            "weaver" => Weaver,
            "wr" or "windranger" => Windranger,
            "winter" or "wyvern" or "ww" or "winterwyvern" => WinterWyvern,
            "wd" or "witchdoctor" => WitchDoctor,
            "wk" or "wraithking" => WraithKing,
            "zeus" => Zeus,
            _ => alias,
        };
    }

    public static string GetImageUrl(this _Hero hero) => $"{SteamCDN}{hero.Portrait}";
    public static string GetImageUrl(this HeroModel hero) => $"{SteamCDN}{hero.Portrait}";
    public static string GetPortraitVideoUrl(this _Hero hero) => $"{SteamCDNVideo}{hero.SteamCdnHeroName()}.webm";

    public static string SteamCdnHeroName(this _Hero hero)
    {
        return hero.Name.Replace("npc_dota_hero_", string.Empty).Trim();
    }

    public static string SteamCdnHeroName(this HeroModel hero)
    {
        return hero.Name.Replace("npc_dota_hero_", string.Empty).Trim();
    }

    public static async Task DownloadImageAsync(string directoryPath, string fileName, Uri uri)
    {
        using var httpClient = new HttpClient();

        // Get the file extension
        var uriWithoutQuery = uri.GetLeftPart(UriPartial.Path);
        var fileExtension = Path.GetExtension(uriWithoutQuery);

        // Create file path and ensure directory exists
        var path = Path.Combine(directoryPath, $"{fileName}{fileExtension}");
        Directory.CreateDirectory(directoryPath);

        // Download the image and write to the file
        var imageBytes = await httpClient.GetByteArrayAsync(uri).ConfigureAwait(false);
        await File.WriteAllBytesAsync(path, imageBytes);
    }
}