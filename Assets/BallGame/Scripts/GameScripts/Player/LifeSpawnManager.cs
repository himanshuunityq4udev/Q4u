using UnityEngine;
public class LifeSpawnManager : MonoBehaviour
{

    [SerializeField] GameObject lifePrefab;
    [SerializeField] GameObject[] lifeSpawnPoints;
    [SerializeField] PlayerData playerData;


    private void OnEnable()
    {
        ActionHelper.SpawnLife += SpawnLife;
    }

    private void OnDisable()
    {
        ActionHelper.SpawnLife -= SpawnLife;

    }

    private void Start()
    {
        SpawnLife();
    }

    private void Update()
    {
        if (playerData.life == playerData.totalLife)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).transform.GetChild(1))
                {
                    bool isCollected = transform.GetChild(i).transform.GetChild(1).gameObject.GetComponent<Life>().IsCollected;
                    GameObject lifeObj = transform.GetChild(i).transform.GetChild(1).gameObject;
                    if (!isCollected)
                    {
                        lifeObj.SetActive(false);
                    }
                }
            }
        }
        if (playerData.life < playerData.totalLife)
        {
            for (int i = 0; i < transform.childCount; i++)
            {

                if (transform.GetChild(i).transform.GetChild(1))
                {
                    bool isCollected = transform.GetChild(i).transform.GetChild(1).gameObject.GetComponent<Life>().IsCollected;
                    GameObject lifeObj = transform.GetChild(i).transform.GetChild(1).gameObject;
                    if (!isCollected)
                    {
                        lifeObj.SetActive(true);
                    }
                }
            }
        }
    }

    //Check if there is need of life or not 
    public void SpawnLife()
    {
        int length = GameObject.FindGameObjectsWithTag("LifeSpawnPoint").Length;

        lifeSpawnPoints = new GameObject[length];

        lifeSpawnPoints = GameObject.FindGameObjectsWithTag("LifeSpawnPoint");
       
            for (int i = 0; i < lifeSpawnPoints.Length; i++) 
            { 
                Instantiate(lifePrefab, lifeSpawnPoints[i].transform.position, lifeSpawnPoints[i].transform.rotation,transform);
            }
       
    }
}
