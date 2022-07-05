namespace BattleShips.Game;

public class OpponentBoardFactory : BoardFactoryBase, IOpponentBoardFactory
{
    public OpponentBoard Create()
    {
        var boardPoints = GenerateEmptyPoints();
        return new OpponentBoard(boardPoints);
    }
}