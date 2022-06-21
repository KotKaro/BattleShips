namespace BattleShips.Game;

public class GameRunner
{
    public void Play()
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
                var coordinates = computerOpponentBoard.GenerateCoordinates();
                WriteMessage($"Computer coordinates are: {coordinates}");

                if (personPlayerBoard.WasShipHit(coordinates))
                {
                    WriteMessage("Computer hits yours ship!");
                    personPlayerBoard.MarkAsHit(coordinates);
                    computerOpponentBoard.MarkAsHit(coordinates);
                }
                else
                {
                    WriteMessage("Computer missed this time, yours turn");
                    computerOpponentBoard.MarkAsMissed(coordinates);
                    playTurn = PlayTurn.Person;
                }
            }
            else
            {
                var userInput = RetrieveUserInput();
                if (userInput?.Equals("Q", StringComparison.OrdinalIgnoreCase) ?? false)
                {
                    WriteMessage("Bye, see you next time!");
                    break;
                }
                
                var coordinates = Coordinates.CoordinatesFromString(userInput, out var errorMessage);
                if (!string.IsNullOrWhiteSpace(errorMessage))
                {
                    WriteMessage(errorMessage);
                    continue;
                }
                
                if (personPlayerBoard.WasShipHit(coordinates))
                {
                    WriteMessage("You hit computers ship!");
                    personPlayerBoard.MarkAsHit(coordinates);
                    computerOpponentBoard.MarkAsHit(coordinates);
                }
                else
                {
                    WriteMessage("You missed this time, computers turn");
                    personOpponentBoard.MarkAsMissed(coordinates);
                    playTurn = PlayTurn.Computer;
                }
            }
            
            Console.Clear();
        }
    }

    protected virtual string? RetrieveUserInput()
    {
        WriteMessage("Your turn, please provide hit coordinates like: A1, C4, H10, in range from A1 to J10");
        WriteMessage("To finish game write: Q");
        var redLine = Console.ReadLine();
        return redLine;
    }

    private PlayTurn DrawFirstPlayer()
    {
        var playTurn = (PlayTurn)Math.Abs(Guid.NewGuid().GetHashCode() % 2);
        switch (playTurn)
        {
            case PlayTurn.Computer:
                WriteMessage("You've got no luck this time! Computer is starting the game");
                break;
            case PlayTurn.Person:
                WriteMessage("Lucky you! You're starting the game");
                break;
        }
        
        return playTurn;
    }
    
    protected virtual void WriteMessage(string message)
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