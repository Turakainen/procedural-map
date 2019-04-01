using System.Collections;
using System.Collections.Generic;

public class Map
{
    public int[,] dataGrid;     // Hold map data where 0 = "WALL" and 1 = "FLOOR"
    

    public Map(int width, int height)
    {
        dataGrid = new int[width, height];
    }

    public int GetWidth()
    {
        return dataGrid.GetLength(0);
    }

    public int GetHeight()
    {
        return dataGrid.GetLength(1);
    }
}
