using Itmo.ObjectOrientedProgramming.Lab3.Weapon;

namespace Itmo.ObjectOrientedProgramming.Lab3.Сreature.Implementation;

public class CustomCreature : BaseCreature
{
    public CustomCreature(string name, int health, IWeapon weapon)
        : base(name, health, weapon) { }
}