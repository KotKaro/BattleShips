using System.Linq;
using BattleShips.Game;
using FluentAssertions;
using Xunit;

namespace BattleShips.Tests.Game;

public class PlayerBoardContentProviderTests
{
    private readonly BoardBuilder _sut;

    public PlayerBoardContentProviderTests()
    {
        _sut = new BoardBuilder();
    }
    
    [Fact]
    public void Generate_ShouldReturn100Points()
    {
        // act
        var result = _sut.Build();
        
        // assert
        result.Length.Should().Be(100);
    }
    
    [Fact]
    public void Generate_AllBoardPointsAreEmpty()
    {
        // act
        var result = _sut.Build();
        
        // assert
        result.All(x => x.PointType == PointType.Empty).Should().BeTrue();
    }
    
    [Fact]
    public void Generate_WithShips_ShouldReturnBoardWith13ShipMarks()
    {
        // act
        var result = _sut.WithShips().Build();
        
        // assert
        result.Count(x => x.PointType == PointType.Ship)
            .Should()
            .Be(13);
    }
}