using Itmo.ObjectOrientedProgramming.Lab3.Weapon;
using Itmo.ObjectOrientedProgramming.Lab3.Сreature;

namespace Itmo.ObjectOrientedProgramming.Lab3.Spell.Implementation;

public class StrengthPotion : ISpell
{
    public ICreature Cast(ICreature creature)
    {
        IWeapon weapon = creature.Weapon;
        weapon.UpdateDamage(weapon.GetDamage() + 5);
        return creature;
    }
}