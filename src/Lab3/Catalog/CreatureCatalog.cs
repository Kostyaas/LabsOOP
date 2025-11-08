using Itmo.ObjectOrientedProgramming.Lab3.Weapon;
using Itmo.ObjectOrientedProgramming.Lab3.Сreature;
using Itmo.ObjectOrientedProgramming.Lab3.Сreature.Fabric;

namespace Itmo.ObjectOrientedProgramming.Lab3.Catalog;

public class CreatureCatalog
{
    private readonly CreatureFactory _factory = new();

    public ICreature CreateAngryFighter() => _factory.AngryFighter();

    public ICreature CreateCombatAnalyst() => _factory.CombatAnalyst();

    public ICreature CreateImmortalHorror() => _factory.ImmortalHorror();

    public ICreature CreateMimicChest() => _factory.MimicChest();

    public ICreature CreateAmuletMaster() => _factory.AmuletMaster();

    public ICreature CreateCustom(int health, IWeapon weapon) => _factory.CreateCustom(health, weapon);

    public IEnumerable<ICreature> CreateAllPredefined()
    {
        return new List<ICreature>
        {
            CreateAngryFighter(),
            CreateCombatAnalyst(),
            CreateImmortalHorror(),
            CreateMimicChest(),
            CreateAmuletMaster(),
        };
    }
}