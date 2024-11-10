using System.Collections;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Item Types")]
    public ItemType collectibleType; // ScriptableObject holding collectible prefabs
    public ItemType nonCollectibleType; // ScriptableObject holding non-collectible prefabs

    [Header("Spawn Points")]
    public Transform[] spawnPoints; // Spawn points for items

    [Header("Spawn Settings")]
    public float spawnInterval = 1.0f;

    private void Start()
    {
        StartCoroutine(SpawnItems());
    }

    private IEnumerator SpawnItems()
    {
        while (true)
        {
            // Select a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Decide randomly whether to spawn a collectible or non-collectible item
            bool spawnCollectible = Random.value < 0.5f;
            GameObject itemToSpawn;

            // Select a random prefab from the chosen item type
            if (spawnCollectible && collectibleType.itemPrefabs.Length > 0)
            {
                itemToSpawn = collectibleType.itemPrefabs[Random.Range(0, collectibleType.itemPrefabs.Length)];
            }
            else if (!spawnCollectible && nonCollectibleType.itemPrefabs.Length > 0)
            {
                itemToSpawn = nonCollectibleType.itemPrefabs[Random.Range(0, nonCollectibleType.itemPrefabs.Length)];
            }
            else
            {
                yield return null; // Skip iteration if no prefabs are available
                continue;
            }

            // Spawn the item at the selected spawn point
            Instantiate(itemToSpawn, spawnPoint.position, Quaternion.identity);

            // Wait for the specified interval before spawning the next item
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
