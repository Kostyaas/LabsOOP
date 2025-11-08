using System.Collections.ObjectModel;

namespace Itmo.ObjectOrientedProgramming.Lab3.Actions;

public class FightResult
{
    private TextWriter Output { get; }

    private readonly List<string> _roundLogs = [];

    public FightResult(TextWriter output)
    {
        Output = output;
    }

    public string Winner { get; set; } = string.Empty;

    public string WinReason { get; set; } = string.Empty;

    public int FirstPlayerSurvivors { get; set; }

    public int SecondPlayerSurvivors { get; set; }

    public ReadOnlyCollection<string> RoundLogs => _roundLogs.AsReadOnly();

    public void AddRoundLog(string log)
    {
        _roundLogs.Add(log); // ✅ Добавляем в приватный список, а не в ReadOnlyCollection
    }

    public void PrintResult()
    {
        Output.WriteLine("=== РЕЗУЛЬТАТ БОЯ ===");
        Output.WriteLine($"Победитель: {Winner}");
        Output.WriteLine($"Причина: {WinReason}");
        Output.WriteLine($"Выжило у Игрока 1: {FirstPlayerSurvivors}");
        Output.WriteLine($"Выжило у Игрока 2: {SecondPlayerSurvivors}");

        Output.WriteLine("\n=== ХОД БОЯ ===");
        foreach (string log in RoundLogs)
        {
            Output.WriteLine(log);
        }
    }
}