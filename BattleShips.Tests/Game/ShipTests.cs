using System;
using System.Drawing;
using System.Linq;
using BattleShips.Game;
using FluentAssertions;
using Xunit;

namespace BattleShips.Tests.Game;

public class ShipTests
{
    [Fact]
    public void Constructor_StartPointIsEqualToEndPoint_ArgumentExceptionIsThrown()
    {
        // act
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new Ship(new BoardPoint(new Point(1, 1), PointType.Ship), new BoardPoint(new Point(1, 1), PointType.Ship));
        });
    }

    [Fact]
    public void GenerateRandom_DistanceBetweenStartAndEndPointIsOneLessThanRequestedLengthBecauseInLengthOf4ThereAre5Cells()
    {
        // arrange
        const int requestedLength = 5;
        
        // act
        var ship = Ship.GenerateRandom(requestedLength);
        
        // assert
        var xDistance = Math.Abs(ship.Start.Point.X - ship.End.Point.X);
        var yDistance = Math.Abs(ship.Start.Point.Y - ship.End.Point.Y);
        new[] { xDistance, yDistance }.Should().Contain(requestedLength - 1);
    }
    
    [Fact]
    public void GenerateRandom_ShipShouldBePlaceInSingleRowOrSingleColumn()
    {
        // arrange
        const int requestedLength = 5;
        
        // act
        var ship = Ship.GenerateRandom(requestedLength);
        
        // assert
        var xDistance = Math.Abs(ship.Start.Point.X - ship.End.Point.X);
        var yDistance = Math.Abs(ship.Start.Point.Y - ship.End.Point.Y);
        new[] { xDistance, yDistance }.Should().Contain(0);
    }

    [Fact]
    public void Iterate_ReturnsAllPointsOfShip()
    {
        // arrange
        var ship = new Ship(new BoardPoint(new Point(2, 2), PointType.Ship), new BoardPoint(new Point(2, 6), PointType.Ship));
        
        // act
        var points = ship.Iterate().ToArray();
        
        // assert
        points.Count().Should().Be(5);
        points.ElementAt(0).Point.Should().Be(new Point(2, 2));
        points.ElementAt(1).Point.Should().Be(new Point(2, 3));
        points.ElementAt(2).Point.Should().Be(new Point(2, 4));
        points.ElementAt(3).Point.Should().Be(new Point(2, 5));
        points.ElementAt(4).Point.Should().Be(new Point(2, 6));
    }
}