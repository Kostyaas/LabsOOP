using Itmo.ObjectOrientedProgramming.Lab3.Weapon;
using Itmo.ObjectOrientedProgramming.Lab3.Weapon.Implementation;

namespace Itmo.ObjectOrientedProgramming.Lab3.Сreature.Implementation;

public class MimicChest : BaseCreature
{
    public MimicChest() : base("MimicChest", 1, new BaseWeapon(1))
    {
    }

    public MimicChest(IWeapon weapon) : base("MimicChest", 1, weapon)
    {
    }

    public override void Attack(ICreature target)
    {
        UpdateHealth(int.Max(target.Health, Health));
        Weapon.UpdateDamage(int.Max(Weapon.GetDamage(), target.Weapon.GetDamage()));
        base.Attack(target);
    }
}