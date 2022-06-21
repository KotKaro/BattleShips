namespace BattleShips.Game;

public class OpponentBoard : Board
{
    public override void MarkAsHit(Coordinates coordinates)
    {
        BoardLines[coordinates.ShipX, coordinates.ShipY] = "O";
    }
}