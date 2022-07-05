using System.Linq;
using BattleShips.Game;
using FluentAssertions;
using Xunit;

namespace BattleShips.Tests.Game;

public class PlayerBoardContentProviderTests
{
    private readonly PlayerBoardContentProvider _sut;

    public PlayerBoardContentProviderTests()
    {
        _sut = new PlayerBoardContentProvider();
    }
    
    [Fact]
    public void Generate_ShouldReturn10x10Board()
    {
        // act
        var result = _sut.Generate();
        
        // assert
        result.Length.Should().Be(100);
    }
    
    [Fact]
    public void Generate_ShouldReturnBoardWith13ShipMarks()
    {
        // act
        var result = _sut.Generate();
        
        // assert
        result.Count(x => x.PointType == PointType.Ship)
            .Should()
            .Be(13);
    }
}