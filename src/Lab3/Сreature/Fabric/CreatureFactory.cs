using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;
using Itmo.ObjectOrientedProgramming.Lab3.Weapon;
using Itmo.ObjectOrientedProgramming.Lab3.Сreature.Implementation;

namespace Itmo.ObjectOrientedProgramming.Lab3.Сreature.Fabric;

public class CreatureFactory
{
    public ICreature AngryFighter()
    {
        return new AngryFighter();
    }

    public ICreature CombatAnalyst()
    {
        return new CombatAnalyst();
    }

    public ICreature ImmortalHorror()
    {
        return new ImmortalHorror();
    }

    public ICreature MimicChest()
    {
        return new MimicChest();
    }

    public ICreature AmuletMaster()
    {
        var master = new AmuletMaster();
        return new AttackMasteryModifier(new MagicShieldModifier(master));
    }

    public ICreature CreateCustom(int health, IWeapon weapon)
    {
        return new CustomCreature("Кастомное существо", health, weapon);
    }

    public ICreature CreateCustom(string name, int health, IWeapon weapon)
    {
        return new CustomCreature(name, health, weapon);
    }

    public ICreature UpdateToAttackMasteryModifier(ICreature creature)
    {
        return new AttackMasteryModifier(creature);
    }

    public ICreature UpdateToMagicShieldModifier(ICreature creature)
    {
        return new MagicShieldModifier(creature);
    }
}