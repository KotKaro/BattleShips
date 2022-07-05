using System.Linq;
using BattleShips.Game;
using FluentAssertions;
using Xunit;

namespace BattleShips.Tests.Game;

public class OpponentBoardFactoryTests
{
    private readonly OpponentBoardFactory _sut;

    public OpponentBoardFactoryTests()
    {
        _sut = new OpponentBoardFactory();
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
    public void Generate_ReturnedBoardGotOnlyEmptyPoints()
    {
        // act
        var result = _sut.Create();
        
        // assert
        result.BoardPoints.All(x => x.PointType == PointType.Empty).Should().BeTrue();
    }
}