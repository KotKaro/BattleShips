using System.Collections.Generic;
using BattleShips.Game;
using BattleShips.Services;

namespace BattleShips.Tests.Services;

public class UserInputProviderStub : IUserInputProvider
{
    public string? GetInput()
    {
        var coordinates = Coordinates.GenerateRandomCoordinates();
        return $"{StringToDigit(coordinates.ShipX)}{coordinates.ShipY}";
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
