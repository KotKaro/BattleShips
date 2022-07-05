using System;
using System.Drawing;
using BattleShips.Game;
using FluentAssertions;
using Xunit;

namespace BattleShips.Tests.Game;

public class BoardPointTests
{
    [Theory]
    [InlineData(-1,1)]
    [InlineData(10,1)]
    [InlineData(2,-1)]
    [InlineData(2,10)]
    public void When_PointIsNotInRangeFrom0To9ForBothCoordinates_ArgumentExceptionThrown(int x, int y)
    {
        Assert.Throws<ArgumentException>(() =>
        {
            _ = new BoardPoint(new Point(x, y), PointType.Empty);
        });
    }
    
    [Fact]
    public void ChangeToShip_PointTypeChangesToShip()
    {
        // arrange
        var boardPoint = new BoardPoint(new Point(1, 1), PointType.Empty);
        
        // act
        boardPoint.ChangeToShip();
        
        // assert
        boardPoint.PointType.Should().Be(PointType.Ship);
    }
    
    [Fact]
    public void ChangeToMissed_PointTypeChangesToMissed()
    {
        // arrange
        var boardPoint = new BoardPoint(new Point(1, 1), PointType.Empty);
        
        // act
        boardPoint.ChangeToMissed();
        
        // assert
        boardPoint.PointType.Should().Be(PointType.Missed);
    }
    
    [Fact]
    public void ChangeToSink_PointTypeChangesToMissed()
    {
        // arrange
        var boardPoint = new BoardPoint(new Point(1, 1), PointType.Empty);
        
        // act
        boardPoint.ChangeToSink();
        
        // assert
        boardPoint.PointType.Should().Be(PointType.Sink);
    }
}