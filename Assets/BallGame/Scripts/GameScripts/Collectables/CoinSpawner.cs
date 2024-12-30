using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab; // Coin prefab to spawn

    /// <summary>
    /// Spawns coins at designated spawn points on the plank.
    /// </summary>
    /// <param name="plankObject">The plank object where coins should be spawned.</param>
    ///

    int count = 0;
    private void Start()
    {
        SpawnCoins();
    }

    public void SpawnCoins()
    {
        if (coinPrefab == null)
        {
            Debug.LogWarning("Coin prefab is not assigned!");
            return;
        }
        if (count == 0)
        {
            Instantiate(coinPrefab, transform);
            count++;
        }
    }
}
