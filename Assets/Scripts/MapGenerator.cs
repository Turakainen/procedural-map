using System;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator
{
    private int seed;          // Seed for generating random numbers
    private Random rand;       // Random class for number generation
    private float wallRatio;   // Percentage of wall to floor ex. 0.1 = 10% of map is walls

    public MapGenerator (int randomSeed)
    {
        seed = randomSeed;
        rand = new Random(seed);
    }

    /// <summary>
    /// Generates and populates new map with given size 
    /// </summary>
    /// <param name="width">Width of map</param>
    /// <param name="height">Height of map</param>
    /// <returns>Map object</returns>
    public Map GenerateMap(int width, int height, float wallToFloorPercent)
    {
        Map map = new Map(width, height);
        wallRatio = 100f * wallToFloorPercent;

        RandomFillMap(map);

        FilterIsolatedWalls(ref map, 0, 3);
        FilterIsolatedWalls(ref map, 0, 3);
        
        FilterIsolatedWalls(ref map, 3, 8);
        FilterIsolatedWalls(ref map, 3, 8);
        
        MakeBoundaries(map);

        return map;
    }

    private void MakeBoundaries(Map map)
    {
        for (int i = 0; i < map.GetWidth(); i++)
        {
            map.dataGrid[0, i] = 0;
            map.dataGrid[i, 0] = 0;

            map.dataGrid[map.GetWidth() - 1, i] = 0;
            map.dataGrid[i, map.GetHeight() - 1] = 0;
        }
    }

    /// <summary>
    /// Applys cellular automata to map with given rules
    /// </summary>
    /// <param name="map">Map</param>
    /// <param name="minWallAmount">Change to floor when neighboring walls less than this</param>
    /// <param name="maxWallAmount">Change to wall when neighboring walls greater than this</param>
    public void FilterIsolatedWalls(ref Map map, int minWallAmount, int maxWallAmount)
    {
        // Create temp map for calculated cells
        Map newMap = new Map(map.GetWidth(), map.GetHeight());
        // Copy data to temp map
        newMap.dataGrid = map.dataGrid;

        // Loop through map cells
        for (int i = 0; i < map.GetWidth(); i++)
        {
            for (int j = 0; j < map.GetHeight(); j++)
            {
                // Check if neighboring walls lower than minium amount
                if (CountNeighboringWalls(map, i, j) < minWallAmount)
                {
                    // Change to floor
                    newMap.dataGrid[i, j] = 1;
                }
                // Check if neighboring walls greater than maxium amount
                if (CountNeighboringWalls(map, i, j) > maxWallAmount)
                {
                    // Change to wall
                    newMap.dataGrid[i, j] = 0;
                }
            }
        }

        map = newMap;
    }

    /// <summary>
    /// Fills map with random noise
    /// </summary>
    /// <param name="map">Map</param>
    public void RandomFillMap(Map map)
    {
        for (int i = 0; i < map.GetWidth(); i++)
        {
            for (int j = 0; j < map.GetHeight(); j++)
            {
                int rng = rand.Next(0, 101);

                if(rng <= wallRatio)
                {
                    map.dataGrid[i, j] = 0;
                } else
                {
                    map.dataGrid[i, j] = 1;
                }
            }
        }
    }

    /// <summary>
    /// Calculates amount of neighbouring walls for given cell in given map
    /// </summary>
    /// <param name="map">Map</param>
    /// <param name="x">X-Coordinate for cell</param>
    /// <param name="y">Y-Coordinate for cell</param>
    /// <returns>Sum of neighboring walls</returns>
    public int CountNeighboringWalls(Map map, int x, int y)
    {
        int sumOfWalls = 0;

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                //Check if index is inside lower boundary
                if (x + i > 0 && y + j > 0)
                {
                    //Check if index is inside upper boundary
                    if(x + i < map.GetWidth() && y + j < map.GetHeight())
                    {
                        if (map.dataGrid[x + i, y + j] == 0) sumOfWalls++;
                    }
                }
            }
        }

        return sumOfWalls;
    }
}
