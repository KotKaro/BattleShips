namespace BattleShips.Game;

public class PlayerBoardFactory : BoardFactoryBase, IPlayerBoardFactory
{
    public PlayerBoard Create()
    {
        var boardPoints = GenerateEmptyPoints();
        AddShip(5, boardPoints);
        AddShip(4, boardPoints);
        AddShip(4, boardPoints);
        return new PlayerBoard(boardPoints);
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