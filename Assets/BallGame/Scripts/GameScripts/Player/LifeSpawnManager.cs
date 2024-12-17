using UnityEngine;
using System;
public class LifeSpawnManager : MonoBehaviour
{

    [SerializeField] GameObject lifePrefab;
    [SerializeField] GameObject[] lifeSpawnPoints;
    [SerializeField] PlayerData playerData;


    private void Start()
    {
        int length = GameObject.FindGameObjectsWithTag("LifeSpawnPoint").Length;

        lifeSpawnPoints = new GameObject[length];

        lifeSpawnPoints = GameObject.FindGameObjectsWithTag("LifeSpawnPoint");

        SpawnLife();
    }

    private void Update()
    {
        if(playerData.life == 4)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

    //Check if there is need of life or not 
    public void SpawnLife()
    {
        if (playerData.life < 4)
        {
            for (int i = 0; i < lifeSpawnPoints.Length; i++) 
            { 
                Instantiate(lifePrefab, lifeSpawnPoints[i].transform.position, lifeSpawnPoints[i].transform.rotation,transform);
            }
        }
    }
}
