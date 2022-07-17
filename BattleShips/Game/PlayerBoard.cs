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

    public bool ContainsShipAtPoint(Coordinates point)
    {
        var boardPoint = BoardPoints.First(x => x.Point.X == point.ShipX && x.Point.Y == point.ShipY);
        return boardPoint.PointType == PointType.Ship;
    }
}