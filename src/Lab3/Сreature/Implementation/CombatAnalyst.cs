using Itmo.ObjectOrientedProgramming.Lab3.Weapon;
using Itmo.ObjectOrientedProgramming.Lab3.Weapon.Implementation;

namespace Itmo.ObjectOrientedProgramming.Lab3.Сreature.Implementation;

public class CombatAnalyst : BaseCreature
{
    public CombatAnalyst() : base("CombatAnalyst", 4, new BaseWeapon(2)) { }

    public CombatAnalyst(IWeapon weapon) : base("CombatAnalyst", 4, weapon) { }

    public override void Attack(ICreature target)
    {
        Weapon.UpdateDamage(Weapon.GetDamage() + 2);
        base.Attack(target);
    }
}