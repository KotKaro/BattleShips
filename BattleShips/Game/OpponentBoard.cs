using BattleShips.Game.Exceptions;
using BattleShips.Services;
using BattleShips.Utils;

namespace BattleShips.Game;

public class OpponentBoard : Board
{
    public OpponentBoard(ILogger logger) : base(logger)
    {        
    }
    
    public override void MarkAsHit(Coordinates coordinates)
    {
        BoardLines[coordinates.ShipX, coordinates.ShipY] = Constants.SinkShipMark;
    }

    public Coordinates GenerateCoordinates()
    {
        while (true)
        {
            var coordinates = Coordinates.GenerateRandomCoordinates();
            var coordinateKnownValue = BoardLines[coordinates.ShipY, coordinates.ShipX];
            if (new[] { Constants.MissedMark, Constants.SinkShipMark }.Contains(coordinateKnownValue))
            {
                continue;
            }

            var anyShipExists = false;
            for (var i = 0; i < Constants.SizeLength; i++)
            {
                var row = BoardLines.GetRow(i);
                var anyShip = row.Any(x => x == Constants.ShipMark);
                if (!anyShip)
                {
                    continue;
                }
                
                anyShipExists = true;
                break;
            }

            if (!anyShipExists)
            {
                throw new NoMoreShipsException();
            }
            
            return coordinates;
        }
    }
}