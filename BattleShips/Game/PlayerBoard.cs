namespace BattleShips.Game;

public class PlayerBoard : Board
{
    public PlayerBoard(BoardPoint[] boardPoints) : base(boardPoints)
    {
    }

    public bool WasShipHit(Coordinates coordinates)
    {
        return GetBoardPointForCoordinates(coordinates).PointType == PointType.Sink;
    }
    
    public override void MarkAsHit(Coordinates coordinates)
    {
        GetBoardPointForCoordinates(coordinates).ChangeToSink();
    }

    public bool ContainsAnyShip()
    {
        return BoardPoints.Any(x => x.PointType == PointType.Ship);
    }

    public bool TryHit(Coordinates point)
    {
        var boardPoint = BoardPoints.First(x => x.Point.X == point.ShipX && x.Point.Y == point.ShipY);
        if (boardPoint.PointType != PointType.Ship)
        {
            return false;
        }
        
        boardPoint.ChangeToSink();
        return true;

    }
}