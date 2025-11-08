namespace Itmo.ObjectOrientedProgramming.Lab3.Weapon;

public interface ICloneWeapon<out T>
{
    IWeapon Clone();
}