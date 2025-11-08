using Itmo.ObjectOrientedProgramming.Lab3.Weapon;
using Itmo.ObjectOrientedProgramming.Lab3.Weapon.Implementation;

namespace Itmo.ObjectOrientedProgramming.Lab3.Сreature.Implementation;

public abstract class BaseCreature : ICreature
{
    protected BaseCreature(string name, int health, IWeapon weapon)
    {
        Name = name;
        Health = health;
        Weapon = weapon;
    }

    protected BaseCreature(string name, int health) : this(name, health, new BaseWeapon(1)) { }

    public string Name { get; }

    public int Health { get; private set; }

    public IWeapon Weapon { get; private set; }

    public bool IsAlive => Health > 0;

    public virtual void Attack(ICreature target)
    {
        if (Health <= 0) return;
        Weapon.Shoot(target);
    }

    public virtual void Damage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }

    public virtual void UpdateHealth(int health)
    {
        Health = health;
    }

    public virtual void UpdateWeapon(IWeapon newWeapon)
    {
        Weapon = newWeapon ?? throw new ArgumentNullException(nameof(newWeapon));
    }

    public ICreature Clone()
    {
        var clone = (BaseCreature)this.MemberwiseClone();

        clone.Weapon = Weapon.Clone();

        return clone;
    }
}