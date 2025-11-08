using Itmo.ObjectOrientedProgramming.Lab3.Сreature;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spell;

public interface ISpellFeature
{
    void Cast(ICreature creature);
}