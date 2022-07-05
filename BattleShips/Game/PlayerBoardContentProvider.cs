using System.Drawing;

namespace BattleShips.Game;

public class PlayerBoardContentProvider : IPlayerBoardContentProvider
{
    public BoardPoint[] Generate()
    {
        var boardPoints = Enumerable.Range(0, 100)
            .Select(x => new BoardPoint(new Point((int)Math.Floor((double)(x / 10)), x % 10), PointType.Empty))
            .ToArray();

        AddShip(5, boardPoints);
        AddShip(4, boardPoints);
        AddShip(4, boardPoints);

        return boardPoints;
    }
    
    private static void AddShip(int shipSize, BoardPoint[] boardPoints)
    {
        var hasShipBeenPlaced = false;
        while (!hasShipBeenPlaced)
        {

            var ship = Ship.GenerateRandom(shipSize);
            var shipCells = ship.Iterate().ToArray();
            var boardPointsToPlaceShip = boardPoints.Where(y => shipCells.Any(z => z.Point.Equals(y.Point)))
                .ToArray();
            var isAnyCellAlreadyOccupied = boardPointsToPlaceShip
                .Any(p => p.PointType == PointType.Ship);
            
            if (isAnyCellAlreadyOccupied)
            {
                continue;
            }
            
            foreach (var boardPoint in boardPointsToPlaceShip)
            {
                boardPoint.ChangeToShip();
            }
            hasShipBeenPlaced = true;
        }
    }
}

public interface IPlayerBoardContentProvider
{
    BoardPoint[] Generate();
}