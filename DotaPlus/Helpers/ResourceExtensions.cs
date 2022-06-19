using DotaPlus.Core.Helpers;
using DotaPlus.Core.Models;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.Windows.ApplicationModel.Resources;
using System.IO;

namespace DotaPlus.Helpers
{
    internal static class ResourceExtensions
    {
        private static readonly ResourceLoader _resourceLoader = new();

        public static ImageSource AttackDamage { get; }
        public static ImageSource Armour { get; }
        public static ImageSource AttackRange { get; }
        public static ImageSource AttackTime { get; }
        public static ImageSource MagicResistance { get; }
        public static ImageSource MovementSpeed { get; }
        public static ImageSource TunrRate { get; }
        public static ImageSource Vision { get; }
        public static ImageSource StrengthAttribute { get; }
        public static ImageSource IntelligenceAttribute { get; }
        public static ImageSource AgilityAttribute { get; }
        public static ImageSource ProjectileSpeed { get; }
        public static ImageSource Cooldown { get; }
        
        static ResourceExtensions()
        {
            AttackDamage = GetImageSource("icon_damage.png");
            Armour = GetImageSource("icon_armor.png");
            AttackRange = GetImageSource("icon_attack_range.png");
            AttackTime = GetImageSource("icon_attack_time.png");
            MagicResistance = GetImageSource("icon_magic_resist.png");
            MovementSpeed = GetImageSource("icon_movement_speed.png");
            TunrRate = GetImageSource("icon_turn_rate.png");
            Vision = GetImageSource("icon_vision.png");
            StrengthAttribute = GetImageSource("hero_strength.png");
            AgilityAttribute = GetImageSource("hero_agility.png");
            IntelligenceAttribute = GetImageSource("hero_intelligence.png");
            ProjectileSpeed = GetImageSource("icon_projectile_speed.png");
            Cooldown = GetImageSource("cooldown.png");
        }

        public static string GetLocalized(this string resourceKey)
        {
            return _resourceLoader.GetString(resourceKey);
        }

        public static ImageSource GetImageSource(string name)
        {
            using var resource = EmbeddedResource.ReadStream(name);
            return GetImageSource(resource);
        }
        
        private static BitmapImage GetImageSource(Stream source)
        {
            var bmp = new BitmapImage();
            bmp.SetSource(source.AsRandomAccessStream());
            return bmp;
        }

        public static ImageSource PrimaryAttribute(this HeroModel hero) => GetAttribute(hero.PrimaryAttribute);

        public static ImageSource GetAttribute(Attribute attribute)
        {
            return attribute switch
            {
                Attribute.Strength => StrengthAttribute,
                Attribute.Agility => AgilityAttribute,
                Attribute.Intelligence => IntelligenceAttribute,
                _ => null,
            };
        }
    }
}
