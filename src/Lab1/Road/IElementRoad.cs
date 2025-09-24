using Itmo.ObjectOrientedProgramming.Lab1.Trasport.Implementation;
using System.Collections.ObjectModel;

namespace Lab1.Road;

public interface IElementRoad
{
    int Lenght { get; }

    ReadOnlyCollection<IRule<Train>> Rules { get; }
}
