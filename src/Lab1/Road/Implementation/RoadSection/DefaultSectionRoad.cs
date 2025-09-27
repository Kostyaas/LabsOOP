using Lab1.Road.Implementation.TypeRule;
using Lab1.Trasport.Implementation;
using System.Collections.ObjectModel;

namespace Lab1.Road.Implementation.RoadSection;

public class DefaultSectionRoad : IElementRoad
{
    public int Lenght { get; }

    public ReadOnlyCollection<IRule<Train>> Rules { get; }

    public DefaultSectionRoad(int len = 5, params IRule<Train>[]? rules)
    {
        Lenght = len;

        var rulesList = new List<IRule<Train>> { new StrengthRule(0) };

        if (rules != null)
        {
            rulesList.AddRange(rules);
        }

        Rules = new ReadOnlyCollection<IRule<Train>>(rulesList);
    }
}