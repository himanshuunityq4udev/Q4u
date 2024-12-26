using UnityEngine;

public class LifeSpawner : MonoBehaviour
{

    public GameObject lifePrefab;


    private void Start()
    {
            SpawnCoins();
    }

    public void SpawnCoins()
    {
        if (lifePrefab == null)
        {
            Debug.LogWarning("Coin prefab is not assigned!");
            return;
        }
        Instantiate(lifePrefab,transform);
    }
   
}
