using UnityEngine;
using System.Collections.Generic;

public class LevelSpawner : MonoBehaviour
{
    public List<GameObject> Levles = new List<GameObject>();

    private void Start()
    {
        SpawnLevel();
    }


    public void SpawnLevel()
    {
        if (PlayerPrefs.GetInt("PlayerLevel") <= Levles.Count)
        {
            Instantiate(Levles[PlayerPrefs.GetInt("PlayerLevel")], transform);
        }
        else
        {
            Instantiate(Levles[Random.Range(0, Levles.Count)], transform);
        }
    }
}
