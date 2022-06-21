using BattleShips.Game;
using BattleShips.Tests.Services;
using Xunit;

namespace BattleShips.Tests.Game;

public class GameRunnerTests
{
    [Fact]
    public void Play_ComputerVsComputer_HundredPlaysGameWithComputerAsHumanPlayer_DoesNotThrowException()
    {
        // Arrange
        var sut = new GameRunner(new LoggerStub(), new UserInputProviderStub());
        
        // Act
        for (var i = 0; i < 100; i++)
        {
            sut.Play();
        }

        // Assert
        Assert.True(true);
    }
}