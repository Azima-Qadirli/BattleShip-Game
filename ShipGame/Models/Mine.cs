using System;

public class Mine
{
    public (int, int) Position { get; set; }

    public Mine(int x,int y)
	{
        Position = (x, y);
    }
}
