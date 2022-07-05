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
}