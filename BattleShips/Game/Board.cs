using System.Text;
using BattleShips.Services;
using BattleShips.Utils;

namespace BattleShips.Game;

public abstract class Board
{
    private readonly ILogger _logger;
    protected readonly string[,] BoardLines;

    
    // TODO: Use some container for DI injection
    protected Board(ILogger logger)
    {
        _logger = logger;
        BoardLines = new string[Constants.SizeLength, Constants.SizeLength];
        FillNumericColumns();
        FillLetterColumns();
        FillEmptySpacesWithPlaceHolders();
    }

    public abstract void MarkAsHit(Coordinates coordinates);

    public void MarkAsMissed(Coordinates coordinates)
    {
        BoardLines[coordinates.ShipY, coordinates.ShipX] = Constants.MissedMark;
    }
    
    
    // TODO: Refactor it: Create Class BoardPrinter
    public void Print()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Players board");
        for (var i = 0; i < Constants.SizeLength; i++)
        {
            var row = BoardLines.GetRow(i);
            stringBuilder.AppendLine($"|{row[0],2}|{row[1],2}|{row[2],2}|{row[3],2}|{row[4],2}|{row[5],2}|{row[6],2}|{row[7],2}|{row[8],2}|{row[9],2}|{row[10],2}|");
        }

        _logger.Log(stringBuilder.ToString());
    }
    
    private void FillNumericColumns()
    {
        for (var i = 1; i < Constants.SizeLength; i++)
        {
            BoardLines[i, 0] = $"{i}";
        }
    }
        
    private void FillLetterColumns()
    {
        const int asciiTableLastIndexBeforeA = 64;
        for (var i = 1; i < Constants.SizeLength; i++)
        {
            BoardLines[0, i] = $"{(char)(asciiTableLastIndexBeforeA + i)}";
        }
    }

    private void FillEmptySpacesWithPlaceHolders()
    {
        for (var i = 1; i < Constants.SizeLength; i++)
        {
            for (var j = 1; j < Constants.SizeLength; j++)
            {
                BoardLines[i, j] = "";
            }
        }
    }
}