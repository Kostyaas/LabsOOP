using Itmo.ObjectOrientedProgramming.Lab3.Spell;
using Itmo.ObjectOrientedProgramming.Lab3.Сreature;

namespace Itmo.ObjectOrientedProgramming.Lab3.Actions;

public class PlayerTable
{
    private readonly List<ICreature> _creatures;

    public PlayerTable(string playerName)
    {
        PlayerName = playerName ?? throw new ArgumentNullException(nameof(playerName));
        MaxCreatures = 7;
        _creatures = [];
    }

    public string PlayerName { get; }

    public int CreatureCount => _creatures.Count;

    public int MaxCreatures { get; }

    public bool IsFull => CreatureCount >= MaxCreatures;

    public bool AddCreature(ICreature creature)
    {
        ArgumentNullException.ThrowIfNull(creature);

        if (IsFull)
            return false;

        _creatures.Add(creature);
        return true;
    }

    public bool RemoveCreature(ICreature creature)
    {
        return _creatures.Remove(creature);
    }

    public void ClearTable()
    {
        _creatures.Clear();
    }

    public IEnumerable<ICreature> GetAttackingCreatures()
    {
        return _creatures.Where(creature => creature.IsAlive).ToList();
    }

    public IEnumerable<ICreature> GetAttackableCreatures()
    {
        return _creatures.Where(creature => creature.IsAlive).ToList();
    }

    public IEnumerable<ICreature> GetAllCreatures()
    {
        return _creatures.Select(creature => creature.Clone()).ToList();
    }

    public bool ApplySpellToCreature(ISpell spell, int index)
    {
        if (index >= 0 && index < MaxCreatures)
        {
            ICreature modifiedCreature = spell.Cast(_creatures[index]);
            _creatures[index] = modifiedCreature;
            return true;
        }

        return false;
    }

    public PlayerTable Copy()
    {
        var copy = new PlayerTable(PlayerName);

        foreach (ICreature creature in _creatures)
        {
            copy._creatures.Add(creature.Clone());
        }

        return copy;
    }
}