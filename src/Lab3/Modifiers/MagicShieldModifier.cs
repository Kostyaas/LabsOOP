using Itmo.ObjectOrientedProgramming.Lab3.Сreature;

namespace Itmo.ObjectOrientedProgramming.Lab3.Modifiers;

public class MagicShieldModifier : CreatureModifier
{
    private bool _isShieldActive = true;

    public MagicShieldModifier(ICreature creature) : base(creature, creature.Name + " MagicShieldModifier")
    {
    }

    public override void Damage(int damage)
    {
        if (_isShieldActive)
        {
            _isShieldActive = false;
            return;
        }

        base.Damage(damage);
    }
}