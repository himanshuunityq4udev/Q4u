using UnityEngine;
using System;
public class LifeManager : MonoBehaviour
{

    [SerializeField] GameObject lifePrefab;
    [SerializeField] GameObject[] lifeSpawnPoints;

    [SerializeField] PlayerData playerData;


    private void Start()
    {
        int length = GameObject.FindGameObjectsWithTag("LifeSpawnPoint").Length;

        lifeSpawnPoints = new GameObject[length];

        lifeSpawnPoints = GameObject.FindGameObjectsWithTag("LifeSpawnPoint");


        NeedLifeOrNot();
    }

    //Check if there is need of life or not 
    public void NeedLifeOrNot()
    {
        if (playerData.life < 4)
        {
            for (int i = 0; i < lifeSpawnPoints.Length; i++)
            {
                Instantiate(lifePrefab, lifeSpawnPoints[i].transform.position, lifeSpawnPoints[i].transform.rotation);
            }
        }
    }
}
