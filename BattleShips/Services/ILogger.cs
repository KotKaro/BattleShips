namespace BattleShips.Services;

public interface ILogger
{
    void Log(string message);
    void Clear();
}