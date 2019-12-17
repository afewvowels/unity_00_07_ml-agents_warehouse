using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareHouseSpawnPoints : MonoBehaviour
{
    public GameObject[] spawnPoints;
    public GameObject[] obstacleSpawnPoints;

    public GameObject[] GetObstacleSpawns(int numObstacles)
    {
        GameObject[] spawnsToReturn = new GameObject[numObstacles];
        List<GameObject> obstacleSpawns = new List<GameObject>();

        foreach (GameObject spawn in obstacleSpawnPoints)
        {
            obstacleSpawns.Add(spawn);
        }

        for (int i = 0; i < numObstacles; i++)
        {
            int randIndex = Random.Range(0, obstacleSpawns.Count - 1);

            if (randIndex > obstacleSpawns.Count - 1)
            {
                randIndex = 0;
            }

            spawnsToReturn[i] = obstacleSpawns[randIndex];
            obstacleSpawns.Remove(obstacleSpawns[randIndex]);
        }

        obstacleSpawns.Clear();

        return spawnsToReturn;
    }
}
