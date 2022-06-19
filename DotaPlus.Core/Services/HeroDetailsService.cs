using DotaPlus.Core.Contracts.Services;
using DotaPlus.Core.Helpers;
using DotaPlus.Core.Models;
using DotaPlus.Core.Models.Internal;
using System.Collections.Generic;
using System.Linq;

namespace DotaPlus.Core.Services;

public class HeroDetailsService : IHeroDetailsService
{
    private readonly IDotaConstants _dotaConstants;

    public HeroDetailsService(IDotaConstants dotaConstants)
    {
        _dotaConstants = dotaConstants;

        foreach (var hero in _dotaConstants.HeroesByName.Values.OrderBy(x => x.LocalizedName))
        {
            var model = new HeroModel();
            model.UpdateHeroInfo(hero);
            var heroAbility = _dotaConstants.HeroAbilities[hero.Name];
            model.UpdateAbilities(heroAbility, _dotaConstants.Abilities);
            model.Lore = GetLore(hero);
            Heroes.Add(model);
        }

    }

    public List<HeroModel> Heroes { get; } = new();
    public HeroModel GetHero(string name) => Heroes.FirstOrDefault(x => x.Name == Helpers.Heroes.FromAlias(name));
    public HeroModel GetHero(int id) => Heroes.FirstOrDefault(x => x.Id == id);
    public bool TryGetHero(string name, out HeroModel hero)
    {
        hero = null;
        if(string.IsNullOrEmpty(name))
        {
            return false;
        }

        var dotaConstantName = Helpers.Heroes.FromAlias(name);
        if (_dotaConstants.HeroesByName.ContainsKey(dotaConstantName) == false)
        {
            return false;
        }

        hero = GetHero(name);
        return true;
    }
    public bool TryGetHero(int? id, out HeroModel hero)
    {
        hero = null;
        if(id.HasValue == false)
        {
            return false;
        }
        
        if(_dotaConstants.HeroesById.ContainsKey(id.Value) == false)
        {
            return false;
        }

        hero = GetHero(id.Value);
        return true;
    }

    private string GetLore(_Hero hero)
    {
        return _dotaConstants.HeroLore[hero.SteamCdnHeroName()];
    }

}
