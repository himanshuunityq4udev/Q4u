using UnityEngine;

public class LifeSpawner : MonoBehaviour
{

    public GameObject lifePrefab;
    int count = 0;

    private void Start()
    {
        SpawnLife();
    }

    public void SpawnLife()
    {
        if (lifePrefab == null)
        {
            Debug.LogWarning("Life prefab is not assigned!");
            return;
        }
        if (count == 0)
        {
            count++;
            Instantiate(lifePrefab, transform);
        }
    }
}
