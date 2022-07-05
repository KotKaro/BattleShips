namespace BattleShips.Game;

public abstract class Board
{
    public BoardPoint[] BoardPoints { get; private set; }
    
    // TODO: Use some container for DI injection
    protected Board(BoardPoint[] boardPoints)
    {
        BoardPoints = boardPoints;
    }

    protected BoardPoint GetBoardPointForCoordinates(Coordinates coordinates)
    {
        return BoardPoints.First(x => x.Point.X == coordinates.ShipX && x.Point.Y == coordinates.ShipY);
    }

    public abstract void MarkAsHit(Coordinates coordinates);

    public void MarkAsMissed(Coordinates coordinates)
    {
        BoardPoints.First(x => x.Point.X == coordinates.ShipX && x.Point.Y == coordinates.ShipY)
                .ChangeToMissed();
    }
}