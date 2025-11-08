using Itmo.ObjectOrientedProgramming.Lab3.Сreature;

namespace Itmo.ObjectOrientedProgramming.Lab3.Weapon;

public interface IWeapon : ICloneWeapon<IWeapon>
{
    void Shoot(ICreature target);

    int GetDamage();

    void UpdateDamage(int damage);
}