using Itmo.ObjectOrientedProgramming.Lab3.Сreature;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spell.Implementation;

public class StaminaPotion : ISpellFeature
{
    public void Cast(ICreature creature)
    {
        creature.UpdateHealth(creature.Health + 5);
    }
}