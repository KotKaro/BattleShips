using System;
using System.Collections.Generic;
using BattleShips.Game;
using Xunit;

namespace BattleShips.Tests.Game;

public class GameRunnerTests
{
    [Fact]
    public void Play_ComputerVsComputer_HundredPlaysGameWithComputerAsHumanPlayer_DoesNotThrowException()
    {
        // Arrange
        var sut = new TestGameRunner();
        
        // Act
        sut.Play();

        
        // Assert
        Assert.True(true);
    }

    private class TestGameRunner : GameRunner
    {
        protected override string? RetrieveUserInput()
        {
            var coordinates = Coordinates.GenerateRandomCoordinates();
            return $"{StringToDigit(coordinates.ShipX)}{coordinates.ShipY}";
        }

        protected override void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static string StringToDigit(int key)
        {
            var valuesDictionary = new Dictionary<int, string>
            {
                { 1, "A" },
                { 2, "B" },
                { 3, "C" },
                { 4, "D" },
                { 5, "E" },
                { 6, "F" },
                { 7, "G" },
                { 8, "H" },
                { 9, "I" },
                { 10, "J" }
            };

            return valuesDictionary[key];
        }
    }
}