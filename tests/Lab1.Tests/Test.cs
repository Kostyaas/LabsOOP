using Lab1.Road.Implementation;
using Lab1.Road.Implementation.RoadSection;
using Lab1.Road.Implementation.TypeRule;
using Lab1.Trasport.Implementation;
using Xunit;

namespace Lab1.Road;

public class Test
{
    [Fact]
    public void DefaultRoadTest()
    {
        var route = new Route(
            new Train(1, 5),
            new DefaultSectionRoad(10),
            new Station(new StopRule(5)));
        bool res = route.Go();
        Assert.False(res);
    }

    [Fact]
    public void PowerRoadTest()
    {
        var route = new Route(
            new Train(1, 5),
            new PowerSectionRoad(5, new StrengthRule(2)),
            new Station(new StopRule(5)));
        bool res = route.Go();
        Assert.True(res);
    }

    [Fact]
    public void PowerAndDefaultRoadTest()
    {
        var route = new Route(
            new Train(1, 5),
            new PowerSectionRoad(5, new StrengthRule(2)),
            new DefaultSectionRoad(10),
            new Station(new StopRule(5)));
        bool res = route.Go();
        Assert.True(res);
    }

    [Fact]
    public void PowerAndDefaultRoadAndStationTest()
    {
        var route = new Route(
            new Train(1, 5),
            new PowerSectionRoad(5, new StrengthRule(3)),
            new Station(new StopRule(5)),
            new DefaultSectionRoad(10),
            new Station(new StopRule(5)));
        bool res = route.Go();
        Assert.False(res);
    }

    [Fact]
    public void EmptyRoute_ShouldReturnTest()
    {
        var route = new Route(
            new Train(1, 5),
            new Station(new StopRule(5)));
        bool res = route.Go();
        Assert.False(res);
    }

    [Fact]
    public void IsCorrectMaxPowerTest()
    {
        var route = new Route(
            new Train(1, 2),
            new PowerSectionRoad(6, new StrengthRule(100000)),
            new Station(new StopRule(6)));
        bool res = route.Go();
        Assert.True(res);
    }

    [Fact]
    public void StationSetPowerAndDefaultRoadTest()
    {
        var route = new Route(
            new Train(1, 5),
            new Station(new StopRule(6), new StrengthRule(2)),
            new DefaultSectionRoad(10),
            new Station(new StopRule(10)));
        bool res = route.Go();
        Assert.True(res);
    }

    [Fact]
    public void MultiRoadTest1()
    {
        var route = new Route(
            new Train(1, 5),
            new PowerSectionRoad(6, new StrengthRule(2)),
            new DefaultSectionRoad(10),
            new DefaultSectionRoad(10, new StrengthRule(-1)),
            new Station(new StopRule(100000)));
        bool res = route.Go();
        Assert.False(res);
    }

    [Fact]
    public void MultiRoadTest2()
    {
        var route = new Route(
            new Train(1, 5),
            new PowerSectionRoad(6, new StrengthRule(2)),
            new DefaultSectionRoad(10),
            new DefaultSectionRoad(8, new StrengthRule(-1)),
            new Station(new StopRule(2)));
        bool res = route.Go();
        Assert.False(res);
    }
}