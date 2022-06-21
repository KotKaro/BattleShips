namespace BattleShips.Utils;

public static class RandomUtils
{
    private static readonly Random Random;

    static RandomUtils()
    {
        Random = new Random();
    }
    
    public static int GetRandomInRange(int start, int stop)
    {
        return Random.Next(start,  stop);
    }
}