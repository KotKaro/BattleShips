namespace BattleShips.Services;

public class Logger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine(message);
        Thread.Sleep(1000);
    }

    public void Clear()
    {
        Console.Clear();
    }
}