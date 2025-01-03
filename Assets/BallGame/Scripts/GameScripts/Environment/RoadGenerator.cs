
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;          // Text to display the current level
    [SerializeField] private Transform startPlankPosition; // Start position for the road
    [SerializeField] private List<GameObject> plainPlanks; // List of plain planks
    [SerializeField] private List<GameObject> coinPlanks;  // List of planks with coins
    [SerializeField] private List<GameObject> lifePlanks;  // List of planks with life
    [SerializeField] private GameObject startPlankPrefab;  // Start plank prefab
    [SerializeField] private GameObject endPlankPrefab;    // End plank prefab
    [SerializeField] private int minPlanks = 10;           // Minimum planks in a road
    [SerializeField] private int maxPlanks = 20;           // Maximum planks in a road

    private int playerLevel = 1;                          // Current player level
    private Transform currentEndTransform;                // Tracks the end position and rotation

    public bool canGenerate = false;


    private void Awake()
    {
        if (!canGenerate)
        {
            playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
            //maxPlanks = PlayerPrefs.GetInt("MaxPlanks", maxPlanks);
        }
    }

    private void Start()
    {
        levelText.text = $"LEVEL {playerLevel}";
        if (canGenerate)
        {
            GenerateRoad();
        }
    }

    private void OnEnable()
    {
        ActionHelper.LevelComplete += IncreasePlayerLevel;
    }

    private void OnDisable()
    {
        ActionHelper.LevelComplete -= IncreasePlayerLevel;
    }

    [ContextMenu("Generate")]
    public void GenerateRoad()
    {
        SetupStartPosition();

        // Clear existing road
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Adjust max planks based on player level
        maxPlanks = Mathf.Clamp(maxPlanks + (playerLevel / 10), minPlanks, 50);

        // Generate road
        if (startPlankPrefab) PlacePlank(startPlankPrefab);
        GenerateMiddlePlanks();
        if (endPlankPrefab) PlacePlank(endPlankPrefab);

        ActionHelper.SpawnLife?.Invoke();
    }

    private void SetupStartPosition()
    {
        if (currentEndTransform == null)
        {
            currentEndTransform = new GameObject("CurrentEnd").transform;
        }
        currentEndTransform.position = startPlankPosition.position;
        currentEndTransform.rotation = startPlankPosition.rotation;
    }


    private void GenerateMiddlePlanks()
    {

        int totalPlanks = Random.Range(minPlanks, maxPlanks + 1);

        // Keep life planks as 2
        int lifePlankCount = 2;
        int remainingPlanks = totalPlanks - lifePlankCount;

        // Ensure equal number of coin and plain planks
        int coinPlankCount = remainingPlanks / 2;
        int plainPlankCount = remainingPlanks - coinPlankCount;

        // Create lists for planks
        List<GameObject> lifePlanksList = new List<GameObject>();
        List<GameObject> coinPlanksList = new List<GameObject>();
        List<GameObject> plainPlanksList = new List<GameObject>();

        // Add planks to respective lists
        for (int i = 0; i < lifePlankCount; i++)
            lifePlanksList.Add(GetRandomPlank(lifePlanks));

        for (int i = 0; i < coinPlankCount; i++)
            coinPlanksList.Add(GetRandomPlank(coinPlanks));

        for (int i = 0; i < plainPlankCount; i++)
            plainPlanksList.Add(GetRandomPlank(plainPlanks));

        // Calculate intervals
        int totalSections = lifePlankCount + 1; // Two life planks create three sections
        int baseInterval = remainingPlanks / totalSections;
        int extraPlanks = remainingPlanks % totalSections;

        // Create the final ordered plank list
        List<GameObject> orderedPlanks = new List<GameObject>();
        int currentIndex = 0;

        // Add starting planks and planks between life planks
        for (int i = 0; i < totalSections; i++)
        {
            int planksInThisSection = baseInterval + (i < extraPlanks ? 1 : 0);

            // Add planks for this section with alternating logic
            bool addCoinNext = true; // Start with coin planks
            // Add planks for this section
            for (int j = 0; j < planksInThisSection; j++)
            {
                if (addCoinNext && coinPlanksList.Count > 0)
                {
                    orderedPlanks.Add(coinPlanksList[0]);
                    coinPlanksList.RemoveAt(0);
                }
                else if (!addCoinNext && plainPlanksList.Count > 0)
                {
                    orderedPlanks.Add(plainPlanksList[0]);
                    plainPlanksList.RemoveAt(0);
                }
                else if (coinPlanksList.Count > 0)
                {
                    // If one type is empty, fall back to the other
                    orderedPlanks.Add(coinPlanksList[0]);
                    coinPlanksList.RemoveAt(0);
                }
                else if (plainPlanksList.Count > 0)
                {
                    orderedPlanks.Add(plainPlanksList[0]);
                    plainPlanksList.RemoveAt(0);
                }

                // Alternate for the next plank
                addCoinNext = !addCoinNext;
            }

            // Add a life plank if not the last section
            if (i < lifePlankCount)
            {
                orderedPlanks.Add(lifePlanksList[currentIndex]);
                currentIndex++;
            }
        }

        // Place the planks
        foreach (var plank in orderedPlanks)
        {
            if (plank != null)
            {
                PlacePlank(plank);
            }
        }
    }


    private void PlacePlank(GameObject plankPrefab)
    {
        GameObject plank = Instantiate(plankPrefab, currentEndTransform.position, currentEndTransform.rotation, transform);

        Transform endTransform = plank.transform.Find("End");
        if (endTransform)
        {
            currentEndTransform.position = endTransform.position;
            currentEndTransform.rotation = endTransform.rotation;
        }
        else
        {
            Debug.LogWarning($"Plank {plankPrefab.name} is missing an 'End' transform.");
        }
    }

    private GameObject GetRandomPlank(List<GameObject> planks)
    {
        if (planks.Count == 0)
        {
            Debug.LogWarning("No planks available in the provided list.");
            return null;
        }
        return planks[Random.Range(0, planks.Count)];
    }

    public void IncreasePlayerLevel()
    {
        playerLevel++;
        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
        //PlayerPrefs.SetInt("MaxPlanks", maxPlanks);
        playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
        levelText.text = $"LEVEL {playerLevel}";
    }
}
