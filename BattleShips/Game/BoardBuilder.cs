using System.Drawing;

namespace BattleShips.Game;

public class BoardBuilder : IBoardBuilder
{
    private readonly BoardPoint[] _boardPoints;

    public BoardBuilder()
    {
        _boardPoints = Enumerable.Range(0, 100)
            .Select(x => new BoardPoint(new Point((int)Math.Floor((double)(x / 10)), x % 10), PointType.Empty))
            .ToArray();
    }
    
    public IBoardBuilder WithShips()
    {
        AddShip(5);
        AddShip(4);
        AddShip(4);
        return this;
    }
    
    public BoardPoint[] Build()
    {
        return _boardPoints;
    }
    
    private void AddShip(int shipSize)
    {
        var hasShipBeenPlaced = false;
        while (!hasShipBeenPlaced)
        {

            var ship = Ship.GenerateRandom(shipSize);
            var shipCells = ship.Iterate().ToArray();
            var boardPointsToPlaceShip = _boardPoints.Where(y => shipCells.Any(z => z.Point.Equals(y.Point)))
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