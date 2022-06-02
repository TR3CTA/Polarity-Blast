using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    public GameObject[] enemyPrefabs;

    private float startDelay = 1;
    public float spawnInterval = 2f;

    private float spawnRange = 47;
    private float spawnRangeY = 25;
    private float spawnPosZ = 47;
    private float spawnPosX = 47;

    private bool stopInvoke;

    // Update is called once per frame
    void Update()
    {
        if (!stopInvoke)
        {
            stopInvoke = true;
            StartSpawning();
        }
    }

    void SpawnRandomEnemy()
    {
        // Decide enemy's spawn point randomly and rotation based on that
        Vector3 northSpawn = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(2, spawnRangeY), spawnPosZ);
        Vector3 eastSpawn = new Vector3(spawnPosX, Random.Range(2, spawnRangeY), Random.Range(-spawnRange, spawnRange));
        Vector3 southSpawn = new Vector3(Random.Range(-spawnRange, spawnRange), Random.Range(2, spawnRangeY), -spawnPosZ);
        Vector3 westSpawn = new Vector3(-spawnPosX, Random.Range(2, spawnRangeY), Random.Range(-spawnRange, spawnRange));

        Vector3[] spawnPositions = new[] { northSpawn, eastSpawn, southSpawn, westSpawn };

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPos = spawnPositions[Random.Range(0, 4)];
        int enemyRotation;

        if (spawnPos == northSpawn)
        {
            enemyRotation = 180;
        }
        else if (spawnPos == eastSpawn)
        {
            enemyRotation = 270;
        }
        else if (spawnPos == southSpawn)
        {
            enemyRotation = 0;
        }
        else
        {
            enemyRotation = 90;
        }

        Instantiate(enemyPrefabs[enemyIndex], spawnPos, Quaternion.Euler(0, enemyRotation, 0));
    }

    void StartSpawning()
    {
        // Spawn enemies on random intervals
        InvokeRepeating("SpawnRandomEnemy", startDelay, spawnInterval);
    }
}
