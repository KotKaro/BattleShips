using System.Drawing;

namespace BattleShips.Game;

public abstract class BoardFactoryBase
{
    protected BoardPoint[] GenerateEmptyPoints()
    {
        return Enumerable.Range(0, 100)
            .Select(x => new BoardPoint(new Point((int)Math.Floor((double)(x / 10)), x % 10), PointType.Empty))
            .ToArray();
    }
}