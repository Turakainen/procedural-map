using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [Range(0, 1)]
    public float wallRatio;

    void Awake()
    {
        int seed = Random.Range(0, (int)99999);
        MapGenerator mapGen = new MapGenerator(seed);
        Map map = mapGen.GenerateMap(45,45, wallRatio);

        PrintMap(map);
    }

    private void PrintMap(Map map)
    {
        for (int i = 0; i < map.GetWidth(); i++)
        {
            StringBuilder sb = new StringBuilder();
            
            for (int j = 0; j < map.GetHeight(); j++)
            {
                if(map.dataGrid[i, j] == 0)
                {
                    sb.Append("<color=black>");
                } else
                {
                    sb.Append("<color=white>");
                }

                sb.Append(map.dataGrid[i, j] + "   ");
                sb.Append("</color>");
            }

            Debug.Log(sb.ToString());
        }
    }
}
