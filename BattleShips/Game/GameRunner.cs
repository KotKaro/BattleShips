namespace BattleShips.Game;

public class GameRunner
{
    public static void Play()
    {
        var personPlayerBoard = new PlayerBoard();
        var personOpponentBoard = new OpponentBoard();
        
        var computerPlayerBoard = new PlayerBoard();
        var computerOpponentBoard = new OpponentBoard();

        Console.Clear();
        PlayTurn? playTurn = DrawFirstPlayer();
        while (personPlayerBoard.ContainsAnyShip() && computerPlayerBoard.ContainsAnyShip())
        {
            personPlayerBoard.Print();
            personOpponentBoard.Print();
            if (playTurn == PlayTurn.Computer)
            {
                var coordinates = Coordinates.GenerateRandomCoordinates();
                Console.WriteLine($"Computer coordinates are: {coordinates}");
                Thread.Sleep(1000);

                if (personPlayerBoard.WasShipHit(coordinates))
                {
                    WriteMessageAndWaitOneSecond("Computer hits yours ship!");
                    personPlayerBoard.MarkAsHit(coordinates);
                    computerOpponentBoard.MarkAsHit(coordinates);
                }
                else
                {
                    WriteMessageAndWaitOneSecond("Computer missed this time, yours turn");
                    playTurn = PlayTurn.Person;
                }
            }
            else
            {
                Console.WriteLine("Your turn, please provide hit coordinates like: A1, C4, H10, in range from A1 to J10");
                Console.WriteLine("To finish game write: Q");
                var redLine = Console.ReadLine();

                if (redLine?.Equals("Q", StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    WriteMessageAndWaitOneSecond("Bye, see you next time!");
                    break;
                }
                
                var coordinates = Coordinates.CoordinatesFromString(redLine, out var errorMessage);
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    WriteMessageAndWaitOneSecond(errorMessage);
                    continue;
                }
                
                if (personPlayerBoard.WasShipHit(coordinates))
                {
                    WriteMessageAndWaitOneSecond("You hit computers ship!");
                    personPlayerBoard.MarkAsHit(coordinates);
                    computerOpponentBoard.MarkAsHit(coordinates);
                }
                else
                {
                    WriteMessageAndWaitOneSecond("You missed this time, computers turn");
                    personOpponentBoard.MarkAsMissed(coordinates);
                    playTurn = PlayTurn.Computer;
                }
            }
            
            Console.Clear();
        }
    }

    private static PlayTurn DrawFirstPlayer()
    {
        var playTurn = (PlayTurn)Math.Abs(Guid.NewGuid().GetHashCode() % 2);
        switch (playTurn)
        {
            case PlayTurn.Computer:
                Console.WriteLine("You've got no luck this time! Computer is starting the game");
                break;
            case PlayTurn.Person:
                Console.WriteLine("Lucky you! You're starting the game");
                break;
        }
        
        return playTurn;
    }
    
    private static void WriteMessageAndWaitOneSecond(string message)
    {
        Console.WriteLine(message);
        Thread.Sleep(1000);
    }
}

public enum PlayTurn
{
    Computer = 0,
    Person = 1
}