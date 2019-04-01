using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMapGizmos : MonoBehaviour
{
    public float cellSize = 1f;
    public int seed;
    [Range(0, 1)]
    public float wallRatio;

    private Map map;

    private void OnDrawGizmos()
    {
        MapGenerator mapGen = new MapGenerator(seed);
        map = mapGen.GenerateMap(45, 45, wallRatio);

        for (int i = 0; i < map.GetWidth(); i++)
        {
            for (int j = 0; j < map.GetHeight(); j++)
            {
                if (map.dataGrid[i, j] == 0)
                {
                    Gizmos.color = new Color(.75f, .65f, .61f, 1);
                }
                else
                {
                    Gizmos.color = Color.grey;
                }

                Gizmos.DrawCube(new Vector3(i * cellSize, cellSize * (1 - map.dataGrid[i, j]), j * cellSize), new Vector3(cellSize, cellSize, cellSize));
            }
        }
    }

    [ContextMenu("Generate new map")]
    private void GenerateNewMap()
    {
        seed = GetRandomSeed();
    }

    private int GetRandomSeed()
    {
        return Random.Range(0, (int)999999);
    }
}
