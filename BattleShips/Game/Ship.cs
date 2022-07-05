using System.Drawing;

namespace BattleShips.Game;

public class Ship
{
    public BoardPoint Start { get; private set; }
    public BoardPoint End { get; private set; }

        
    public Ship(BoardPoint start, BoardPoint end)
    {
        if (start.Point.Equals(end.Point))
        {
            throw new ArgumentException("Start point cannot be equal to end point");
        }

        Start = start;
        End = end;
    }

    public IEnumerable<BoardPoint> Iterate()
    {
        for (var i = Math.Min(Start.Point.X, End.Point.X); i <= Math.Max(Start.Point.X, End.Point.X); i++)
        {
            for (var j = Math.Min(Start.Point.Y, End.Point.Y); j <= Math.Max(Start.Point.Y, End.Point.Y); j++)
            {
                yield return new BoardPoint(new Point(i, j), PointType.Ship);
            }
        } 
    }

    public static Ship GenerateRandom(int shipLength)
    {
        const int differenceBetweenOccupiedSpaceAndSubtractionResult = 1;
        var realShipLength = shipLength - differenceBetweenOccupiedSpaceAndSubtractionResult;
        var direction = Random.Shared.Next(0, 3);
        var start = new Point(Random.Shared.Next(1, 10), Random.Shared.Next(1, 10));
        var end = new Point(start.X, start.Y);
        switch (direction)
        {
            case 0:
                end.X += realShipLength;
                break;
            case 1:
                end.X -= realShipLength;
                break;
            case 2:
                end.Y += realShipLength;
                break;
            case 3:
                end.Y -= realShipLength;
                break;
        }

        try
        {
            return new Ship(new BoardPoint(start, PointType.Ship), new BoardPoint(end, PointType.Ship));
        }
        catch (ArgumentException)
        {
            return GenerateRandom(shipLength);
        }
    }
}