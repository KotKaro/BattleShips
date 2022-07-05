namespace BattleShips.Game;

public interface IBoardBuilder
{
    IBoardBuilder WithShips();
    BoardPoint[] Build();
}