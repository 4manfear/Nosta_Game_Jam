using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Manager : MonoBehaviour
{
    public GameObject scoreObjectPrefab;
    public GameObject bombPrefab;
    public Transform[] spawnPoints; // Assign three spawners in the inspector
    public float spawnInterval = 1.5f; // Interval between spawns

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Randomly select a spawn point
            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[spawnIndex];

            // Randomly decide between score object or bomb
            GameObject prefabToSpawn = Random.value > 0.5f ? scoreObjectPrefab : bombPrefab;
            Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }
}
