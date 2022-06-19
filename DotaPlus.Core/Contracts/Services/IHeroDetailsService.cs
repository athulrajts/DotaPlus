using DotaPlus.Core.Models;
using System.Collections.Generic;

namespace DotaPlus.Core.Contracts.Services
{
    public interface IHeroDetailsService
    {
        List<HeroModel> Heroes { get; }
        HeroModel GetHero(string heroName);
        HeroModel GetHero(int id);
        bool TryGetHero(string heroName, out HeroModel hero);
        bool TryGetHero(int? id, out HeroModel hero);
    }
}
