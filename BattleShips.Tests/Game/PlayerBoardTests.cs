using System.Linq;
using BattleShips.Game;
using FluentAssertions;
using Xunit;

namespace BattleShips.Tests.Game;

public class PlayerBoardTests
{
    private readonly PlayerBoard _sut;

    public PlayerBoardTests()
    {
        _sut = new PlayerBoardFactory().Create();
    }

    [Fact]
    public void TryHit_DoesNotHaveShipAtProvidedCoordinates_ReturnFalseAndPointIsUnmodified()
    {
        // Arrange
        var firstEmptyPoint = _sut.BoardPoints.First(x => x.PointType == PointType.Empty);
        
        // Act
        var result = _sut.TryHit(new Coordinates(firstEmptyPoint.Point.X, firstEmptyPoint.Point.Y));
        
        // Assert
        result.Should().BeFalse();
        _sut.BoardPoints.First(x => x == firstEmptyPoint).PointType.Should().Be(PointType.Empty);
    }
    
    [Fact]
    public void TryHit_DoesHaveShipAtProvidedCoordinates_ReturnTrueAndPointIsSink()
    {
        // Arrange
        var firstEmptyPoint = _sut.BoardPoints.First(x => x.PointType == PointType.Ship);
        
        // Act
        var result = _sut.TryHit(new Coordinates(firstEmptyPoint.Point.X, firstEmptyPoint.Point.Y));
        
        // Assert
        result.Should().BeTrue();
        _sut.BoardPoints.First(x => x == firstEmptyPoint).PointType.Should().Be(PointType.Sink);
    }
}