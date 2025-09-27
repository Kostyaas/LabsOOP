using Lab1.Road.Implementation;
using Lab1.Road.Implementation.RoadSection;
using Lab1.Road.Implementation.TypeRule;
using Lab1.Trasport.Implementation;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab1.Tests;

public class Test
{
    [Fact]
    public void Route_TrainWithLowPower_DefaultRoad_ShouldNotCompleteRoute()
    {
        var route = new Route(
            new Train(1, 5),
            new DefaultSectionRoad(10),
            new Station(new StopRule(5)));
        bool result = route.IsGo();
        Assert.False(result);
    }

    [Fact]
    public void Route_TrainWithSufficientPower_PowerRoad_ShouldCompleteRoute()
    {
        var route = new Route(
            new Train(1, 5),
            new PowerSectionRoad(5, new StrengthRule(2)),
            new Station(new StopRule(5)));
        bool result = route.IsGo();
        Assert.True(result);
    }

    [Fact]
    public void Route_TrainWithPower_PowerAndDefaultRoad_ShouldCompleteRoute()
    {
        var route = new Route(
            new Train(1, 5),
            new PowerSectionRoad(5, new StrengthRule(2)),
            new DefaultSectionRoad(10),
            new Station(new StopRule(5)));
        bool result = route.IsGo();
        Assert.True(result);
    }

    [Fact]
    public void Route_TrainWithInsufficientPower_PowerDefaultRoadAndStation_ShouldNotCompleteRoute()
    {
        var route = new Route(
            new Train(1, 5),
            new PowerSectionRoad(5, new StrengthRule(3)),
            new Station(new StopRule(5)),
            new DefaultSectionRoad(10),
            new Station(new StopRule(5)));
        bool result = route.IsGo();
        Assert.False(result);
    }

    [Fact]
    public void Route_EmptyRouteWithStation_ShouldNotCompleteRoute()
    {
        var route = new Route(
            new Train(1, 5),
            new Station(new StopRule(5)));
        bool result = route.IsGo();
        Assert.False(result);
    }

    [Fact]
    public void Route_TrainWithLowPower_StrongPowerRoad_ShouldCompleteRoute()
    {
        var route = new Route(
            new Train(1, 2),
            new PowerSectionRoad(6, new StrengthRule(100000)),
            new Station(new StopRule(6)));
        bool result = route.IsGo();
        Assert.True(result);
    }

    [Fact]
    public void Route_TrainWithPower_StationWithPowerAndDefaultRoad_ShouldCompleteRoute()
    {
        var route = new Route(
            new Train(1, 5),
            new Station(new StopRule(6), new StrengthRule(2)),
            new DefaultSectionRoad(10),
            new Station(new StopRule(10)));
        bool result = route.IsGo();
        Assert.True(result);
    }

    [Fact]
    public void Route_TrainWithPower_MultipleRoadsWithNegativeStrength_ShouldNotCompleteRoute()
    {
        var route = new Route(
            new Train(1, 5),
            new PowerSectionRoad(6, new StrengthRule(2)),
            new DefaultSectionRoad(10),
            new DefaultSectionRoad(10, new StrengthRule(-1)),
            new Station(new StopRule(100000)));
        bool result = route.IsGo();
        Assert.False(result);
    }

    [Fact]
    public void Route_TrainWithPower_MultipleRoadsWithInsufficientCharge_ShouldNotCompleteRoute()
    {
        var route = new Route(
            new Train(1, 5),
            new PowerSectionRoad(6, new StrengthRule(2)),
            new DefaultSectionRoad(10),
            new DefaultSectionRoad(8, new StrengthRule(-1)),
            new Station(new StopRule(2)));
        bool result = route.IsGo();
        Assert.False(result);
    }
}