using Itmo.ObjectOrientedProgramming.Lab3.Weapon;

namespace Itmo.ObjectOrientedProgramming.Lab3.Сreature;

public interface ICreature : ICreaturePrototype<ICreature>
{
    string Name { get; }

    int Health { get; }

    IWeapon Weapon { get; }

    bool IsAlive { get; }

    void Attack(ICreature target);

    void Damage(int damage);

    void UpdateHealth(int health);

    void UpdateWeapon(IWeapon newWeapon);
}