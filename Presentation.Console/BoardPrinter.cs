using System.Text;
using BattleShips.Game;

namespace Presentation.Console;

public class BoardPrinter
{
    private readonly Board _board;

    public BoardPrinter(Board board)
    {
        _board = board;
    }
    
    public void Print()
    {
        var lines = new string[11][];
        for (var i = 0; i < lines.Length; i++)
        {
            lines[i] = new string[11];
        }
        
        lines[0] = new[] { "", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        lines[1][0] = "1";
        lines[2][0] = "2";
        lines[3][0] = "3";
        lines[4][0] = "4";
        lines[5][0] = "5";
        lines[6][0] = "6";
        lines[7][0] = "7";
        lines[8][0] = "8";
        lines[9][0] = "9";
        lines[10][0] = "10";
        
        foreach (var boardPoint in _board.BoardPoints)
        {
            lines[boardPoint.Point.X + 1][boardPoint.Point.Y + 1] = boardPoint.PointType switch
            {
                PointType.Empty => "",
                PointType.Missed => Constants.MissedMark,
                PointType.Ship => Constants.ShipMark,
                PointType.Sink => Constants.SinkShipMark,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
        
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Players board");
        foreach (var row in lines)
        {
            stringBuilder.AppendLine($"|{row[0],2}|{row[1],2}|{row[2],2}|{row[3],2}|{row[4],2}|{row[5],2}|{row[6],2}|{row[7],2}|{row[8],2}|{row[9],2}|{row[10],2}|");
        }

        System.Console.WriteLine(stringBuilder.ToString());
    }
}