using BattleShips.Services;

namespace BattleShips.Tests.Services;

public class LoggerStub : ILogger
{
    public void Log(string message)
    {
    }

    public void Clear()
    {
    }
}