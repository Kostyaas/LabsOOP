using Lab1.Trasport.Implementation;
using System.Collections.ObjectModel;

namespace Lab1.Road.Implementation.RoadSection;

public class PowerSectionRoad : IElementRoad
{
    public int Lenght { get; } = 0;

    public ReadOnlyCollection<IRule<Train>> Rules { get; }

    public PowerSectionRoad(int lenght, params IRule<Train>[] rules)
    {
        Lenght = lenght;
        Rules = new ReadOnlyCollection<IRule<Train>>(rules);
    }
}