public class Ship
{
    public int Length { get; private set; }
    public List<(int, int)> Positions { get; private set; }

    public Ship(int length)
    {
        Length = length;
        Positions = new List<(int, int)>();
    }

    public void AddPosition(int x, int y)
    {
        Positions.Add((x, y));
    }
}
