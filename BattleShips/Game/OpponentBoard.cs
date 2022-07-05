using BattleShips.Game.Exceptions;
using BattleShips.Services;

namespace BattleShips.Game;

public class OpponentBoard : Board
{
    public OpponentBoard(BoardPoint[] boardPoints) : base(boardPoints)
    {        
    }
    
    public override void MarkAsHit(Coordinates coordinates)
    {
        BoardPoints.First(x => x.Point.X == coordinates.ShipX && x.Point.Y == coordinates.ShipY).ChangeToSink();
    }

    public Coordinates GenerateCoordinates()
    {
        if (BoardPoints.All(b => b.PointType != PointType.Ship))
        {
            throw new NoMoreShipsException();
        }
        
        var boardPoints = BoardPoints.Where(x => x.PointType != PointType.Missed && x.PointType != PointType.Sink)
            .ToArray();

        var point = boardPoints[Random.Shared.Next(0, boardPoints.Length - 1)];
        return new Coordinates(point.Point.X + 1, point.Point.Y + 1);
    }
}