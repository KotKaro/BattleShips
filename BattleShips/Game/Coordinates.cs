using BattleShips.Utils;

namespace BattleShips.Game;

public class Coordinates
{
    public int ShipX { get; private set; }

    public int ShipY { get; private set; }


    public static Coordinates GenerateRandomCoordinates()
    {
        return new Coordinates(
            GenerateRandomNumberInRangeFromOneToSizeLength(),
            GenerateRandomNumberInRangeFromOneToSizeLength()
        );
    }

    public Coordinates(int shipX, int shipY)
    {
        if (shipX is > Constants.SizeLength or < 1 || shipY is > Constants.SizeLength or < 1)
        {
            throw new ArgumentException("Provided coordinates are out of game board!");
        }

        ShipX = shipX;
        ShipY = shipY;
    }

    public override string ToString()
    {
        return $"Coordinates are - X: {ShipX} and Y: {ShipY}";
    }

    private static int GenerateRandomNumberInRangeFromOneToSizeLength()
    {
        return RandomUtils.GetRandomInRange(1, Constants.SizeLength);
    }

    public static Coordinates? CoordinatesFromString(string text, out string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            errorMessage = "You passed empty coordinates, please try again!";
            return null;
        }
                
        if (text.Length > 3)
        {
            errorMessage = "You passed too long coordinates, please try again!";
            return null;
        }

        var letterValue = (int)text[0].ToString().ToUpper().ToCharArray()[0];

        if (letterValue is < 65 or > 75)
        {
            errorMessage = "Incorrect A-J letter provided, please try again!";
            return null;
        }

        var coordinateX = letterValue == 74 ? 10 : int.Parse(((char)(letterValue - 16)).ToString());
        var isSecondPartDigit = int.TryParse(text.Substring(1), out var coordinateY);

        if (!isSecondPartDigit)
        {
            errorMessage = "Incorrect input, please try again!";
            return null;
        }
        
        if (coordinateY == 0)
        {
            errorMessage = "Digit coordinate cannot be 0, please try again!";
            return null;
        }
        
        if (coordinateY >= Constants.SizeLength)
        {
            errorMessage = $"Digit coordinate is greater than board size, please provide value in range from 0 to {Constants.SizeLength - 1}!";
            return null;
        }

        errorMessage = "";
        return new Coordinates(coordinateX, coordinateY);
    }
}