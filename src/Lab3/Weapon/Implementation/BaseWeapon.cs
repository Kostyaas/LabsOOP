using Itmo.ObjectOrientedProgramming.Lab3.Сreature;

namespace Itmo.ObjectOrientedProgramming.Lab3.Weapon.Implementation;

public class BaseWeapon : IWeapon
{
    private int Damage { get; set; }

    public BaseWeapon(int damage)
    {
        Damage = damage;
    }

    public IWeapon Clone()
    {
        return new BaseWeapon(Damage);
    }

    public void Shoot(ICreature target)
    {
        target.Damage(Damage);
    }

    public int GetDamage()
    {
        return Damage;
    }

    public void UpdateDamage(int damage)
    {
        Damage = damage;
    }
}