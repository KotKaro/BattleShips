using System.Drawing;

namespace BattleShips.Game;

public class BoardPoint 
{
    public PointType PointType { get; private set; }
    public Point Point { get; private set; }
    
    public BoardPoint(Point point, PointType pointType)
    {
        if (new[] { point.X, point.Y }.Any(x => x is < 0 or > 9))
        {
            throw new ArgumentException("Ship must be located in 0-9 coordinates");
        }

        Point = point;
        PointType = pointType;
    }

    public void ChangeToShip()
    {
        PointType = PointType.Ship;
    }

    public override string ToString()
    {
        return $"({Point.X},{Point.Y}) {PointType}";
    }
}