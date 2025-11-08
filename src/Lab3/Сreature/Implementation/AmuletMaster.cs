using Itmo.ObjectOrientedProgramming.Lab3.Weapon;
using Itmo.ObjectOrientedProgramming.Lab3.Weapon.Implementation;

namespace Itmo.ObjectOrientedProgramming.Lab3.Сreature.Implementation;

public class AmuletMaster : BaseCreature
{
    public AmuletMaster() : base("Amulet Master", 2, new BaseWeapon(5)) { }

    public AmuletMaster(IWeapon weapon) : base("Amulet Master", 2, weapon) { }
}