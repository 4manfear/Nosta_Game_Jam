using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] collectiblePrefabs;
    public GameObject[] nonCollectiblePrefabs;
    public Transform[] spawnPoints;
    public float spawnInterval = 2f;

    void Start()
    {
        // Start the spawning loop
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            // Wait for the next spawn interval
            yield return new WaitForSeconds(spawnInterval);

            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Decide randomly whether the object is collectible or non-collectible
            bool isCollectible = Random.value > 0.5f;

            GameObject[] prefabArray = isCollectible ? collectiblePrefabs : nonCollectiblePrefabs;

            // Pick a random prefab from the chosen array
            GameObject selectedObject = prefabArray[Random.Range(0, prefabArray.Length)];

            // Instantiate the object at the spawn point
            GameObject spawnedObject = Instantiate(selectedObject, spawnPoint.position, Quaternion.identity);

            // Optionally, add the object to a list for later management
            // objectsList.Add(spawnedObject);
        }
    }
}
