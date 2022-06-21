using BattleShips.Services;
using BattleShips.Utils;

namespace BattleShips.Game;

public class PlayerBoard : Board
{
    public PlayerBoard(ILogger logger) : base(logger)
    {        
        AddShips();
    }

    public bool WasShipHit(Coordinates coordinates)
    {
        return BoardLines[coordinates.ShipY, coordinates.ShipX] == Constants.ShipMark;
    }
    
    public override void MarkAsHit(Coordinates coordinates)
    {
        BoardLines[coordinates.ShipY, coordinates.ShipX] = Constants.SinkShipMark;
    }

    public bool ContainsAnyShip()
    {
        for (var i = 1; i < Constants.SizeLength; i++)
        {
            for (var j = 1; j < Constants.SizeLength; j++)
            {
                if (BoardLines[i, j] == Constants.ShipMark)
                {
                    return true;
                }
            }
        }

        return false;
    }
    
    private void AddShips()
    {
        AddShip(4);
        AddShip(3);
        AddShip(3);
    }
    
    private void AddShip(int shipSize)
    {
        var hasShipBeenPlaced = false;
        while (!hasShipBeenPlaced)
        {
            var randomCoordinates = Coordinates.GenerateRandomCoordinates();
            var shipXLocation = randomCoordinates.ShipX;
            var shipYLocation = randomCoordinates.ShipY;
            var shipXLocationFinish = shipXLocation;
            var shipYLocationFinish = shipYLocation;
            
            SetShipDirection(shipSize, ref shipXLocationFinish, ref shipYLocationFinish);

            if (AreShipEndCoordinatesOverflowBoard(shipXLocationFinish, shipYLocationFinish))
            {
                continue;
            }

            if (!AreAllShipCellsFree(shipXLocation, shipXLocationFinish, shipYLocation, shipYLocationFinish))
            {
                continue;
            }
            
            PlaceShipOnBoard(shipXLocation, shipXLocationFinish, shipYLocation, shipYLocationFinish);
            hasShipBeenPlaced = true;
        }
    }

    private void PlaceShipOnBoard(int shipXLocation, int shipXLocationFinish, int shipYLocation, int shipYLocationFinish)
    {
        foreach (var (shipX,shipY) in IterateThroughShipCells(shipXLocation, shipXLocationFinish, shipYLocation, shipYLocationFinish))
        {
            BoardLines[shipX, shipY] = "X";
        }
    }

    private bool AreAllShipCellsFree(int shipXLocation, int shipXLocationFinish, int shipYLocation, int shipYLocationFinish)
    {
        foreach (var (shipX,shipY) in IterateThroughShipCells(shipXLocation, shipXLocationFinish, shipYLocation, shipYLocationFinish))
        {
            if (!string.IsNullOrWhiteSpace(BoardLines[shipX, shipY]))
            {
                return false;
            }
        }
        
        return true;
    }

    private static void SetShipDirection(int shipSize, ref int shipXLocationFinish, ref int shipYLocationFinish)
    {
        var direction = RandomUtils.GetRandomInRange(0, 3);
        switch (direction)
        {
            case 0:
                shipXLocationFinish -= shipSize;
                break;
            case 1:
                shipYLocationFinish -= shipSize;
                break;
            case 2:
                shipXLocationFinish += shipSize;
                break;
            case 3:
                shipYLocationFinish += shipSize;
                break;
        }
    }
    
    private static IEnumerable<(int,int)> IterateThroughShipCells(int shipXLocation, int shipXLocationFinish, int shipYLocation, int shipYLocationFinish)
    {
        var lesserXCoordinate = shipXLocation > shipXLocationFinish ? shipXLocationFinish : shipXLocation;
        var greaterXCoordinate = shipXLocation > shipXLocationFinish ? shipXLocation : shipXLocationFinish;
        var lesserYCoordinate = shipYLocation > shipYLocationFinish ? shipYLocationFinish : shipYLocation;
        var greaterYCoordinate = shipYLocation > shipYLocationFinish ? shipYLocation : shipYLocationFinish;
        
        for (var i = lesserXCoordinate; i <= greaterXCoordinate; i++)
        {
            for (var j = lesserYCoordinate; j <= greaterYCoordinate; j++)
            {
                yield return (i, j);
            }
        }
    }

    private static bool AreShipEndCoordinatesOverflowBoard(int shipXLocationFinish, int shipYLocationFinish)
    {
        return shipXLocationFinish is < 1 or >= Constants.SizeLength || shipYLocationFinish is < 1 or >= Constants.SizeLength;
    }
}