using Itmo.ObjectOrientedProgramming.Lab3.Weapon;
using Itmo.ObjectOrientedProgramming.Lab3.Weapon.Implementation;

namespace Itmo.ObjectOrientedProgramming.Lab3.Сreature.Implementation;

public class ImmortalHorror : BaseCreature
{
    private bool _isFirstDie;

    public ImmortalHorror() : base("ImmortalHorror", 4, new BaseWeapon(4))
    {
        _isFirstDie = true;
    }

    public ImmortalHorror(IWeapon weapon) : base("ImmortalHorror", 4, weapon)
    {
        _isFirstDie = true;
    }

    public override void Damage(int damage)
    {
        base.Damage(damage);
        if (Health > 0 || !_isFirstDie) return;
        _isFirstDie = false;
        UpdateHealth(1);
    }
}