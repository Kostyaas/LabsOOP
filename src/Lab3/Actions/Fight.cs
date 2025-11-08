using Itmo.ObjectOrientedProgramming.Lab3.Сreature;
using System.Security.Cryptography;

namespace Itmo.ObjectOrientedProgramming.Lab3.Actions;

public class Fight
{
    private readonly PlayerTable _first;
    private readonly PlayerTable _second;

    public Fight(PlayerTable first, PlayerTable second)
    {
        _first = first.Copy();
        _second = second.Copy();
    }

    public FightResult Run()
    {
        var result = new FightResult(Console.Out);
        int round = 1;

        while (BothPlayersHaveAliveCreatures() && round <= 50)
        {
            result.AddRoundLog($"=== Раунд {round} ===");

            ExecuteAttackRound(_first, _second, "Игрок 1", "Игрок 2", result, round);

            if (!_second.GetAttackableCreatures().Any())
                break;

            ExecuteAttackRound(_second, _first, "Игрок 2", "Игрок 1", result, round);

            if (!_first.GetAttackableCreatures().Any())
                break;

            round++;
        }

        DetermineWinner(result);
        return result;
    }

    private bool BothPlayersHaveAliveCreatures()
    {
        return _first.GetAttackableCreatures().Any() && _second.GetAttackableCreatures().Any();
    }

    private void ExecuteAttackRound(
        PlayerTable attackerTable,
        PlayerTable defenderTable,
        string attackerName,
        string defenderName,
        FightResult result,
        int round)
    {
        var attackers = attackerTable.GetAttackingCreatures().ToList();
        var defenders = defenderTable.GetAttackableCreatures().ToList();

        if (attackers.Count == 0 || defenders.Count == 0)
            return;

        foreach (ICreature? attacker in attackers)
        {
            if (!defenders.Any(d => d.IsAlive))
                break;

            var aliveDefenders = defenders.Where(d => d.IsAlive).ToList();
            var random = new Random();
            int targetIndex = RandomNumberGenerator.GetInt32(0, aliveDefenders.Count);
            ICreature target = aliveDefenders[targetIndex];
            int damageBefore = target.Health;
            attacker.Attack(target);
            int damageDealt = damageBefore - target.Health;

            result.AddRoundLog($"{attackerName}: {attacker.Name} атакует {defenderName}: {target.Name} " +
                               $"(урон: {damageDealt}, осталось HP: {target.Health})");

            // Если защитник умер, логируем это
            if (!target.IsAlive)
            {
                result.AddRoundLog($"⚰ {defenderName}: {target.Name} погиб!");
            }
        }
    }

    private void DetermineWinner(FightResult result)
    {
        int firstAlive = _first.GetAttackableCreatures().Count();
        int secondAlive = _second.GetAttackableCreatures().Count();

        if (firstAlive > secondAlive)
        {
            result.Winner = "Игрок 1";
            result.WinReason = $"Осталось существ: {firstAlive} против {secondAlive}";
        }
        else if (secondAlive > firstAlive)
        {
            result.Winner = "Игрок 2";
            result.WinReason = $"Осталось существ: {secondAlive} против {firstAlive}";
        }
        else
        {
            result.Winner = "Ничья";
            result.WinReason = $"Оба игрока имеют по {firstAlive} выживших существ";
        }

        result.FirstPlayerSurvivors = firstAlive;
        result.SecondPlayerSurvivors = secondAlive;
    }
}