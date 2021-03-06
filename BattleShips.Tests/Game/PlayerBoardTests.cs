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
    public void ContainsShipAtPoint_DoesNotHaveShipAtProvidedCoordinates_ReturnFalse()
    {
        // Arrange
        var firstEmptyPoint = _sut.BoardPoints.First(x => x.PointType == PointType.Empty);
        
        // Act
        var result = _sut.ContainsShipAtPoint(new Coordinates(firstEmptyPoint.Point.X, firstEmptyPoint.Point.Y));
        
        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void ContainsShipAtPoint_DoesHaveShipAtProvidedCoordinates_ReturnTrue()
    {
        // Arrange
        var firstEmptyPoint = _sut.BoardPoints.First(x => x.PointType == PointType.Ship);
        
        // Act
        var result = _sut.ContainsShipAtPoint(new Coordinates(firstEmptyPoint.Point.X, firstEmptyPoint.Point.Y));
        
        // Assert
        result.Should().BeTrue();
    }
}