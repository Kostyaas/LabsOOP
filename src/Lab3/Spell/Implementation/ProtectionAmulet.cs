using Itmo.ObjectOrientedProgramming.Lab3.Modifiers;
using Itmo.ObjectOrientedProgramming.Lab3.Сreature;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spell.Implementation;

public class ProtectionAmulet : ISpellModifier
{
    public ICreature Cast(ICreature creature)
    {
        return new MagicShieldModifier(creature);
    }
}