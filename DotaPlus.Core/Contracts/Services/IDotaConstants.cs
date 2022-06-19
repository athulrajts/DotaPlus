using DotaPlus.Core.Models;
using DotaPlus.Core.Models.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotaPlus.Core.Contracts.Services;

public interface IDotaConstants
{
    IReadOnlyDictionary<int, _Hero> HeroesById { get; }
    IReadOnlyDictionary<string, _Hero> HeroesByName { get; }
    IReadOnlyDictionary<string, string> HeroLore { get; }
    IReadOnlyDictionary<string, _HeroAbilities> HeroAbilities { get; }
    IReadOnlyDictionary<string, _Ability> Abilities { get; }
    IReadOnlyDictionary<string, ItemModel> Items { get; }
    IReadOnlyDictionary<long, ItemModel> ItemsById { get; }
    Task Initialize();
}
