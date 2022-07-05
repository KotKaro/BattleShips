using System.Linq;
using BattleShips.Game;
using FluentAssertions;
using Xunit;

namespace BattleShips.Tests.Game;

public class PlayerBoardBuilderTests
{
    private readonly PlayerBoardFactory _sut;

    public PlayerBoardBuilderTests()
    {
        _sut = new PlayerBoardFactory();
    }
    
    [Fact]
    public void Generate_ShouldReturn100Points()
    {
        // act
        var result = _sut.Create();
        
        // assert
        result.BoardPoints.Length.Should().Be(100);
    }
    
    [Fact]
    public void Generate_ShouldReturnBoardWith13ShipMarks()
    {
        // act
        var result = _sut.Create();
        
        // assert
        result.BoardPoints.Count(x => x.PointType == PointType.Ship)
            .Should()
            .Be(13);
    }
}