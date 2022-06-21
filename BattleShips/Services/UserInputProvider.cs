namespace BattleShips.Services;

public class UserInputProvider : IUserInputProvider
{
    public string? GetInput()
    {
        return Console.ReadLine();
    }
}