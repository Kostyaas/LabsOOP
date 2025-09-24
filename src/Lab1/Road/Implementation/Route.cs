using Lab1.Trasport.Implementation;
using System.Collections.ObjectModel;

namespace Lab1.Road.Implementation;

public class Route
{
    private readonly Train _train;

    private readonly ReadOnlyCollection<IElementRoad> _elements;

    public Route(Train train, params IElementRoad[] elements)
    {
        _elements = new ReadOnlyCollection<IElementRoad>(elements);
        _train = train;
    }

    public bool Go()
    {
        int indexRoad = 0;
        bool isGoodRoad = true;
        if (_elements.Count == 0)
        {
            Console.WriteLine("Маршрут пуст! Неудача.");
            return false;
        }

        isGoodRoad &= ApplyRule(_elements[indexRoad]);
        int tempPath = _elements[indexRoad].Lenght;
        while (_train.Move() && indexRoad < _elements.Count)
        {
            tempPath -= _train.Speed;
            if (tempPath > 0) continue;
            ++indexRoad;
            if (indexRoad >= _elements.Count) continue;
            isGoodRoad &= ApplyRule(_elements[indexRoad]);
            tempPath = _elements[indexRoad].Lenght;
        }

        return (_train.Move() || indexRoad >= _elements.Count) && isGoodRoad;
    }

    private bool ApplyRule(IElementRoad road)
    {
        return road.Rules.Aggregate(true, (current, rule) => current & rule.Apply(_train));
    }
}