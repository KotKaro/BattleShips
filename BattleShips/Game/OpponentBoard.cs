namespace BattleShips.Game;

public class OpponentBoard : Board
{
    public override void MarkAsHit(Coordinates coordinates)
    {
        BoardLines[coordinates.ShipX, coordinates.ShipY] = Constants.SinkShipMark;
    }

    public Coordinates GenerateCoordinates()
    {
        var coordinates = Coordinates.GenerateRandomCoordinates();
        var coordinateKnownValue = BoardLines[coordinates.ShipY, coordinates.ShipX];
        return new[] { Constants.MissedMark, Constants.SinkShipMark }.Contains(coordinateKnownValue) 
            ? GenerateCoordinates() 
            : coordinates;
    }
}