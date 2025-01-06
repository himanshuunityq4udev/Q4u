using UnityEngine;
using System.Collections.Generic;
using TMPro;
public class LevelSpawner : MonoBehaviour
{
    //public List<GameObject> Levels = new List<GameObject>();

    public Levels levels;

    public TMP_Dropdown dropdown;
    public bool isTestLevel = false;
    [SerializeField] private TMP_Text levelText;          // Text to display the current level

    private int currentLevel = 0;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("Level"))
        {
            PlayerPrefs.SetInt("Level", currentLevel);
            Debug.Log("Current level if" + PlayerPrefs.GetInt("Level"));

        }
        else
        {
            currentLevel = PlayerPrefs.GetInt("Level");
            Debug.Log("Current level else" + currentLevel);
        }
    }

    private void OnEnable()
    {
        ActionHelper.GenerateNewLevel += SpawnLevel;
        ActionHelper.LevelComplete += IncreaseLevel;

    }
    private void OnDisable()
    {
        ActionHelper.GenerateNewLevel -= SpawnLevel;
        ActionHelper.LevelComplete -= IncreaseLevel;
    }
    private void Start()
    {
        levelText.text = $"LEVEL {currentLevel + 1}";
        if (!isTestLevel)
        {
            Debug.Log("Call");
            SpawnLevel();
            dropdown.gameObject.SetActive(false);
        }
        else
        {
            dropdown.value = currentLevel;
            dropdown.gameObject.SetActive(true);
            TestLevelSpawn();
        }
    }
    public void SpawnLevel()
    {
        if (!isTestLevel)
        {
            if (transform.childCount > 0)
            {
                Destroy(transform.GetChild(0).gameObject);
            }

            if (currentLevel < levels.gameLevels.Count)
            {
                Instantiate(levels.gameLevels[currentLevel], transform);
            }
            else
            {
                int lev = Random.Range(0, levels.gameLevels.Count);
                Instantiate(levels.gameLevels[lev], transform);
            }
            ActionHelper.SpawnLife?.Invoke();
        }

    }

    public void TestLevelSpawn()
    {
        if(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        
        }
        if (currentLevel <= dropdown.options.Count)
        {
            Debug.Log("Call Huaas");
            Instantiate(levels.gameLevels[dropdown.value], transform);
            currentLevel = dropdown.value;
            levelText.text = $"LEVEL {currentLevel + 1}";
        }

         ActionHelper.SpawnLife?.Invoke();
    }


    public void IncreaseLevel()
    {
        currentLevel++;
        PlayerPrefs.SetInt("Level", currentLevel);
        Debug.Log("Current level" + PlayerPrefs.GetInt("Level"));
        levelText.text = $"LEVEL {currentLevel + 1}";
        if (isTestLevel)
        {
            dropdown.value = currentLevel;
        }
    }
}
