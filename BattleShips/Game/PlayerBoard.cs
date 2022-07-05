using BattleShips.Services;

namespace BattleShips.Game;

public class PlayerBoard : Board
{
    public PlayerBoard(ILogger logger, IPlayerBoardContentProvider playerBoardContentProvider) : base(logger)
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
        // TODO: Refactor it to: PlayerBoardContentProvider - place there all methods and test it
    }
}

public enum PointType
{
    Empty = 1,
    Sink = 2,
    Ship = 3
}