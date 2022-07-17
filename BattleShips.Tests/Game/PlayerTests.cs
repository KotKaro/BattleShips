using System.Linq;
using BattleShips.Game;
using FluentAssertions;
using Xunit;

namespace BattleShips.Tests.Game;

public class PlayerTests
{
    private readonly OpponentBoardFactory _factory;

    public PlayerTests()
    {
        _factory = new OpponentBoardFactory();
    }
    
    [Fact]
    public void HasAnyShip_PlayersDoesNotHaveAnyShip_ReturnsFalse()
    {
        // Arrange
        var sut = new Player(new PlayerBoard(_factory.Create().BoardPoints), _factory.Create());
        
        // Act
        var result = sut.HasAnyShip();
        
        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void HasAnyShip_PlayersDoesHaveOneShip_ReturnsTrue()
    {
        // Arrange
        var points = _factory.Create().BoardPoints;
        points.First().ChangeToShip();
        
        var sut = new Player(new PlayerBoard(points), _factory.Create());
        
        // Act
        var result = sut.HasAnyShip();
        
        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Constructor_PlayerGotRandomGuidAssigned()
    {
        // Act
        var sut = new Player(new PlayerBoard(_factory.Create().BoardPoints), _factory.Create());
        
        // Assert
        sut.Id.Should().NotBeEmpty();
    }
    
    [Fact]
    public void MarkHitOnOpponentBoard_ChangesPointOnOpponentBoardToSink()
    {
        // Arrange
        var coordinates = Coordinates.GenerateRandomCoordinates();
        var opponentBoard = _factory.Create();
        var sut = new Player(new PlayerBoard(_factory.Create().BoardPoints), opponentBoard);
        
        // Act
        sut.MarkHitOnOpponentBoard(coordinates);
        
        // Assert
        opponentBoard.BoardPoints.First(x => x.Point.X == coordinates.ShipX && x.Point.Y == coordinates.ShipY)
            .PointType
            .Should()
            .Be(PointType.Sink);
    }
    
    [Fact]
    public void MarkMissOnOpponentBoard_ChangesPointOnOpponentBoardToMissed()
    {
        // Arrange
        var coordinates = Coordinates.GenerateRandomCoordinates();
        var opponentBoard = _factory.Create();
        var sut = new Player(new PlayerBoard(_factory.Create().BoardPoints), opponentBoard);
        
        // Act
        sut.MarkMissOnOpponentBoard(coordinates);
        
        // Assert
        opponentBoard.BoardPoints.First(x => x.Point.X == coordinates.ShipX && x.Point.Y == coordinates.ShipY)
            .PointType
            .Should()
            .Be(PointType.Missed);
    }
    
    [Fact]
    public void MarkAsHitAtOwnBoard_ChangesPointOnOwnBoardToMissed()
    {
        // Arrange
        var coordinates = Coordinates.GenerateRandomCoordinates();
        var boardPoints = _factory.Create().BoardPoints;
        var sut = new Player(new PlayerBoard(boardPoints), _factory.Create());
        
        // Act
        sut.MarkAsHitAtOwnBoard(coordinates);
        
        // Assert
        boardPoints.First(x => x.Point.X == coordinates.ShipX && x.Point.Y == coordinates.ShipY)
            .PointType
            .Should()
            .Be(PointType.Sink);
    }
}