using UnityEngine;
public class LifeSpawnManager : MonoBehaviour
{

    //[SerializeField] GameObject lifePrefab;
    //[SerializeField] GameObject[] lifeSpawnPoints;
    //[SerializeField] PlayerData playerData;


    //private void OnEnable()
    //{
    //    ActionHelper.SpawnLife += SpawnLife;
    //}

    //private void OnDisable()
    //{
    //    ActionHelper.SpawnLife -= SpawnLife;

    //}


    private void Start()
    {
        //SpawnLife();
    }

    //private void Update()
    //{
    //    // If the player has full health, deactivate all uncollected life objects.
    //    if (playerData.life == playerData.totalLife)
    //    {
    //        for (int i = 0; i < transform.childCount; i++)
    //        {
    //            GameObject lifeObj = transform.GetChild(i).gameObject;
    //            bool isCollected = lifeObj.GetComponent<Life>().IsCollected;

    //            // Deactivate uncollected life objects
    //            if (!isCollected)
    //            {
    //                for (int j = 0; j < lifeObj.transform.childCount - 1; j++)
    //                {

    //                    lifeObj.transform.GetChild(j).gameObject.SetActive(false);
    //                    foreach (GameObject child in lifeObj.transform.GetChild(j))
    //                    {
    //                        child.gameObject.GetComponent<SphereCollider>().enabled = false;
    //                    }
    //                }
    //            }
    //        }
    //    }
    //    else if (playerData.life < playerData.totalLife)
    //    {
    //        // Activate relevant life objects if player's life is not full
    //        for (int i = 0; i < transform.childCount; i++)
    //        {
    //            GameObject lifeObj = transform.GetChild(i).gameObject;
    //            bool isCollected = lifeObj.GetComponent<Life>().IsCollected;

    //            // Activate only the specific life ball associated with the player
    //            if (!isCollected)
    //            {
    //                ChangeLifeBallAsPlayer(lifeObj);
    //            }
    //        }
    //    }
    //}

    //private void ChangeLifeBallAsPlayer(GameObject lifeObj)
    //{
    //    for (int j = 0; j < lifeObj.transform.childCount - 1; j++)
    //    {
    //        if (j == PlayerPrefs.GetInt("CurrentBall"))
    //        {
    //            lifeObj.transform.GetChild(j).gameObject.SetActive(true);
    //            lifeObj.transform.GetChild(j).gameObject.GetComponent<SphereCollider>().enabled = true;
    //        }
    //    }
    //}

    //Check if there is need of life or not 
    //public void SpawnLife()
    //{
    //    int length = GameObject.FindGameObjectsWithTag("LifeSpawnPoint").Length;

    //    lifeSpawnPoints = new GameObject[length];

    //    lifeSpawnPoints = GameObject.FindGameObjectsWithTag("LifeSpawnPoint");

    //    for (int i = 0; i < lifeSpawnPoints.Length; i++)
    //    {
    //        Instantiate(lifePrefab, lifeSpawnPoints[i].transform.position, lifeSpawnPoints[i].transform.rotation, transform);
    //    }
    //}
}
