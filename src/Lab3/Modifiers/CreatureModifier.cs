using Itmo.ObjectOrientedProgramming.Lab3.Weapon;
using Itmo.ObjectOrientedProgramming.Lab3.Сreature;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public abstract class CreatureModifier : ICreature
{
    private readonly ICreature _creature;

    public string Name { get; }

    public int Health
    {
        get => _creature.Health;
        private set => _creature.UpdateHealth(value);
    }

    public IWeapon Weapon
    {
        get => _creature.Weapon;
        private set => _creature.UpdateWeapon(value);
    }

    public bool IsAlive => _creature.IsAlive;

    protected CreatureModifier(ICreature creature, string name)
    {
        _creature = creature;
        Name = name;
    }

    public virtual void Attack(ICreature target)
    {
        _creature.Attack(target);
    }

    public virtual void Damage(int damage)
    {
        _creature.Damage(damage);
    }

    public virtual void UpdateHealth(int health)
    {
        _creature.UpdateHealth(health);
    }

    public virtual void UpdateWeapon(IWeapon newWeapon)
    {
        _creature.UpdateWeapon(newWeapon);
    }

    public ICreature Clone()
    {
        var clone = (CreatureModifier)MemberwiseClone();

        clone.Weapon = Weapon.Clone();

        return clone;
    }
}