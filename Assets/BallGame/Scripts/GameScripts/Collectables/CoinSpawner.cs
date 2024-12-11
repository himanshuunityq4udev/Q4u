using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Coin prefab to spawn

    /// <summary>
    /// Spawns coins at designated spawn points on the plank.
    /// </summary>
    /// <param name="plankObject">The plank object where coins should be spawned.</param>
    public void SpawnCoins(GameObject plankObject)
    {
        if (coinPrefab == null)
        {
            Debug.LogWarning("Coin prefab is not assigned!");
            return;
        }

        // Find all child objects named "CoinSpawnPoint"
        foreach (Transform spawnPoint in plankObject.transform)
        {
            if (spawnPoint.name == "CoinSpawnPoint")
            {
                Instantiate(coinPrefab, spawnPoint.position, spawnPoint.rotation, plankObject.transform);
            }
        }
    }
}
