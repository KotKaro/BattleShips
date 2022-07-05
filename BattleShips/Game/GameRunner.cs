using BattleShips.Game.Exceptions;
using BattleShips.Services;
using BattleShips.Utils;

namespace BattleShips.Game;

public class GameRunner
{
    private readonly ILogger _logger;
    private readonly IUserInputProvider _userInputProvider;

    public GameRunner(ILogger logger, IUserInputProvider userInputProvider)
    {
        _logger = logger;
        _userInputProvider = userInputProvider;
    }
    
    public void Play()
    {
        // TODO: Create player abstraction which is going to gather both of those boards
        var personPlayerBoard = new PlayerBoard(new BoardBuilder().WithShips().Build(),_logger);
        var personOpponentBoard = new OpponentBoard(new BoardBuilder().Build(),_logger);
        
        var computerPlayerBoard = new PlayerBoard(new BoardBuilder().WithShips().Build(), _logger);
        var computerOpponentBoard = new OpponentBoard(new BoardBuilder().Build(),_logger);

        _logger.Clear();
        var playTurn = DrawFirstPlayer();
        while (personPlayerBoard.ContainsAnyShip() && computerPlayerBoard.ContainsAnyShip())
        {
            personPlayerBoard.Print();
            personOpponentBoard.Print();

            try
            {
                playTurn = playTurn == PlayTurn.Computer 
                    ? ExecuteComputerPlay(computerOpponentBoard, personPlayerBoard) 
                    : ExecuteHumanPlayTurn(personPlayerBoard, computerOpponentBoard, personOpponentBoard);
            }
            catch (NoMoreShipsException)
            {
                break;
            }
            
            if (playTurn == PlayTurn.End)
            {
                break;
            }
            
            _logger.Clear();
        }

        if (personPlayerBoard.ContainsAnyShip())
        {
            _logger.Log("Congratulations, you won!");
        }

        if (computerPlayerBoard.ContainsAnyShip())
        {
            _logger.Log("Unfortunately, this time computer won!");
        }
    }

    // TODO: Create match abstraction
    private PlayTurn ExecuteHumanPlayTurn(PlayerBoard personPlayerBoard, OpponentBoard computerOpponentBoard, OpponentBoard personOpponentBoard)
    {
        _logger.Log("Your turn, please provide hit coordinates like: A1, C4, H10, in range from A1 to J10");
        _logger.Log("To finish game write: Q");
        var userInput = _userInputProvider.GetInput();
        if (userInput?.Equals("Q", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            _logger.Log("Bye, see you next time!");
            return PlayTurn.End;
        }

        var coordinates = Coordinates.CoordinatesFromString(userInput, out var errorMessage);
        if (!string.IsNullOrWhiteSpace(errorMessage))
        {
            _logger.Log(errorMessage);
            return PlayTurn.Person;
        }

        if (personPlayerBoard.WasShipHit(coordinates))
        {
            _logger.Log("You hit computers ship!");
            personPlayerBoard.MarkAsHit(coordinates);
            computerOpponentBoard.MarkAsHit(coordinates);
        }
        else
        {
            _logger.Log("You missed this time, computers turn");
            personOpponentBoard.MarkAsMissed(coordinates);
            return PlayTurn.Computer;
        }

        return PlayTurn.Person;
    }

    // TODO: Create match abstraction
    private PlayTurn ExecuteComputerPlay(OpponentBoard computerOpponentBoard, PlayerBoard personPlayerBoard)
    {
        var coordinates = computerOpponentBoard.GenerateCoordinates();
        _logger.Log($"Computer coordinates are: {coordinates}");

        if (personPlayerBoard.WasShipHit(coordinates))
        {
            _logger.Log("Computer hits yours ship!");
            personPlayerBoard.MarkAsHit(coordinates);
            computerOpponentBoard.MarkAsHit(coordinates);
        }
        else
        {
            _logger.Log("Computer missed this time, yours turn");
            computerOpponentBoard.MarkAsMissed(coordinates);
            return PlayTurn.Person;
        }

        return PlayTurn.Person;
    }

    // TODO: Create some drawer abstraction
    private PlayTurn DrawFirstPlayer()
    {
        var playTurn = (PlayTurn)RandomUtils.GetRandomInRange(0, 1);
        switch (playTurn)
        {
            case PlayTurn.Computer:
                _logger.Log("You've got no luck this time! Computer is starting the game");
                break;
            case PlayTurn.Person:
                _logger.Log("Lucky you! You're starting the game");
                break;
        }
        
        return playTurn;
    }
}