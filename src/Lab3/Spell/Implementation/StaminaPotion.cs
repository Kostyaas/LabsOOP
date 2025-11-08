using Itmo.ObjectOrientedProgramming.Lab3.Сreature;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spell.Implementation;

public class StaminaPotion : ISpell
{
    public ICreature Cast(ICreature creature)
    {
        creature.UpdateHealth(creature.Health + 5);
        return creature;
    }
}