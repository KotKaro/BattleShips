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
        
        var sut = new Player(new PlayerBoard(_factory.Create().BoardPoints), _factory.Create());
        
        // Act
        var result = sut.HasAnyShip();
        
        // Assert
        result.Should().BeTrue();
    }
}