using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class LevelSpawner : MonoBehaviour
{
    public List<GameObject> Levles = new List<GameObject>();

    public TMP_Dropdown dropdown;
    public bool isTestLevel = false;
    private void Start()
    {
        if (!isTestLevel)
        {
            SpawnLevel();
            dropdown.gameObject.SetActive(false);
        }
        else
        {
            dropdown.gameObject.SetActive(true);
            TestLevelSpawn();
        }
    }
    public void SpawnLevel()
    {
        Debug.Log("Current level" + PlayerPrefs.GetInt("PlayerLevel"));

        if (PlayerPrefs.GetInt("PlayerLevel") < Levles.Count)
        {
            Instantiate(Levles[PlayerPrefs.GetInt("PlayerLevel")], transform);
        }
        else
        {
            Instantiate(Levles[Random.Range(0, Levles.Count)], transform);
        }
        ActionHelper.SpawnLife?.Invoke();

    }

    public void TestLevelSpawn()
    {
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        
        }

       Instantiate(Levles[dropdown.value], transform);

        ActionHelper.SpawnLife?.Invoke();
    }
}
