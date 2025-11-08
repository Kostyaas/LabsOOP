using Itmo.ObjectOrientedProgramming.Lab3.Сreature;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public class AttackMasteryModifier : CreatureModifier
{
    public AttackMasteryModifier(ICreature creature) : base(creature, creature.Name + " AttackMasteryModifier")
    {
    }

    public override void Attack(ICreature target)
    {
        base.Attack(target);

        if (Health > 0)
        {
            base.Attack(target);
        }
    }
}