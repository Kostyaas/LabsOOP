using Itmo.ObjectOrientedProgramming.Lab3.Weapon;
using Itmo.ObjectOrientedProgramming.Lab3.Weapon.Implementation;

namespace Itmo.ObjectOrientedProgramming.Lab3.Сreature.Implementation;

public class AngryFighter : BaseCreature
{
    public AngryFighter() : base("AngryFighter", 6, new BaseWeapon(1)) { }

    public AngryFighter(IWeapon weapon) : base("AngryFighter", 6, weapon) { }

    public override void Damage(int damage)
    {
        Weapon.UpdateDamage(Weapon.GetDamage() * 2);

        base.Damage(damage);
    }
}