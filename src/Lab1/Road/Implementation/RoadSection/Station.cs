using Lab1.Road.Implementation.TypeRule;
using Lab1.Trasport.Implementation;
using System.Collections.ObjectModel;

namespace Lab1.Road.Implementation.RoadSection;

public class Station : IElementRoad
{
    public int Lenght { get; } = 0;

    public ReadOnlyCollection<IRule<Train>> Rules { get; }

    public Station(params IRule<Train>[]? rules)
    {
        var rulesList = new List<IRule<Train>> { new StrengthRule(0) };

        if (rules != null)
        {
            rulesList.AddRange(rules);
        }

        Rules = new ReadOnlyCollection<IRule<Train>>(rulesList);
    }
}