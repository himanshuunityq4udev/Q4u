
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
            maxPlanks = PlayerPrefs.GetInt("MaxPlanks", maxPlanks);
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

            // Add planks for this section
            for (int j = 0; j < planksInThisSection; j++)
            {
                if (coinPlanksList.Count > 0)
                    orderedPlanks.Add(coinPlanksList[0]);
                else if (plainPlanksList.Count > 0)
                    orderedPlanks.Add(plainPlanksList[0]);

                // Remove the plank after adding
                if (coinPlanksList.Count > 0) coinPlanksList.RemoveAt(0);
                else if (plainPlanksList.Count > 0) plainPlanksList.RemoveAt(0);
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
   
    //private void ShuffleList<T>(List<T> list)
    //{
    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        int randomIndex = Random.Range(i, list.Count);
    //        T temp = list[i];
    //        list[i] = list[randomIndex];
    //        list[randomIndex] = temp;
    //    }
    //}

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
        PlayerPrefs.SetInt("MaxPlanks", maxPlanks);
    }
}



//using System.Collections.Generic;
//using UnityEngine;
//using TMPro;

//public class RoadGenerator : MonoBehaviour
//{
//    [SerializeField] private TMP_Text levelText;          // Text to display the current level
//    [SerializeField] private Transform startPlankPosition; // Start position for the road
//    [SerializeField] private List<GameObject> allPlanks;   // List of all plank prefabs
//    [SerializeField] private GameObject startPlankPrefab;  // Start plank prefab
//    [SerializeField] private GameObject endPlankPrefab;    // End plank prefab
//    [SerializeField] private int minPlanks = 5;            // Minimum planks in a road
//    [SerializeField] private int maxPlanks = 10;           // Maximum planks in a road

//    private int playerLevel = 1;                          // Current player level
//    private Transform currentEndTransform;                // Tracks the end position and rotation
//    private Dictionary<GameObject, int> plankCounts;      // Tracks counts of placed planks

//    private void Awake()
//    {
//        // Load player level and max planks from PlayerPrefs
//        playerLevel = PlayerPrefs.GetInt("PlayerLevel", 1);
//        maxPlanks = PlayerPrefs.GetInt("MaxPlanks", maxPlanks);
//    }

//    private void Start()
//    {
//        levelText.text = $"LEVEL {playerLevel}";
//        GenerateRoad();
//    }

//    private void OnEnable()
//    {
//        ActionHelper.LevelComplete += IncreasePlayerLevel;
//    }

//    private void OnDisable()
//    {
//        ActionHelper.LevelComplete -= IncreasePlayerLevel;
//    }

//    [ContextMenu("Generate")]
//    public void GenerateRoad()
//    {
//        // Initialize plank counts and prepare for road generation
//        InitializePlankCounts();

//        // Set starting position
//        SetupStartPosition();

//        // Clear existing road
//        foreach (Transform child in transform)
//        {
//            Destroy(child.gameObject);
//        }

//        // Adjust max planks based on player level
//        maxPlanks = Mathf.Clamp(maxPlanks + (playerLevel / 10), minPlanks, 50);

//        // Generate road
//        if (startPlankPrefab) PlacePlank(startPlankPrefab);
//        GenerateMiddlePlanks();
//        if (endPlankPrefab) PlacePlank(endPlankPrefab);

//        ActionHelper.SpawnLife?.Invoke();
//    }

//    private void InitializePlankCounts()
//    {
//        plankCounts = new Dictionary<GameObject, int>();

//        foreach (var plankPrefab in allPlanks)
//        {
//            plankCounts[plankPrefab] = 0;
//        }

//        if (startPlankPrefab) plankCounts[startPlankPrefab] = 0;
//        if (endPlankPrefab) plankCounts[endPlankPrefab] = 0;
//    }

//    private void SetupStartPosition()
//    {
//        if (currentEndTransform == null)
//        {
//            currentEndTransform = new GameObject("CurrentEnd").transform;
//        }
//        currentEndTransform.position = startPlankPosition.position;
//        currentEndTransform.rotation = startPlankPosition.rotation;
//    }

//    private void GenerateMiddlePlanks()
//    {
//        int plankCount = Random.Range(minPlanks, maxPlanks + 1);
//        for (int i = 0; i < plankCount; i++)
//        {
//            GameObject randomPlank = GetRandomPlankForLevel(playerLevel);
//            if (randomPlank) PlacePlank(randomPlank);
//        }
//    }

//    private void PlacePlank(GameObject plankPrefab)
//    {
//        GameObject plank = Instantiate(plankPrefab, currentEndTransform.position, currentEndTransform.rotation, transform);
//        plankCounts[plankPrefab]++;

//        Transform endTransform = plank.transform.Find("End");
//        if (endTransform)
//        {
//            currentEndTransform.position = endTransform.position;
//            currentEndTransform.rotation = endTransform.rotation;
//        }
//        else
//        {
//            Debug.LogWarning($"Plank {plankPrefab.name} is missing an 'End' transform.");
//        }
//    }

//    public void IncreasePlayerLevel()
//    {
//        playerLevel++;
//        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
//        PlayerPrefs.SetInt("MaxPlanks", maxPlanks);
//    }

//    private GameObject GetRandomPlankForLevel(int level)
//    {
//        int maxPlankIndex = Mathf.Clamp(level / 10 * 5, 5, allPlanks.Count);

//        List<GameObject> eligiblePlanks = new List<GameObject>();
//        for (int i = 0; i < maxPlankIndex; i++)
//        {
//            GameObject plank = allPlanks[i];
//            if (plankCounts[plank] < 2)
//            {
//                eligiblePlanks.Add(plank);
//            }
//        }

//        if (eligiblePlanks.Count == 0)
//        {
//            Debug.LogWarning("No eligible planks available.");
//            return null;
//        }

//        return eligiblePlanks[Random.Range(0, eligiblePlanks.Count)];
//    }
//}

////using System.Collections.Generic;
////using UnityEngine;
////using TMPro;

////public class RoadGenerator : MonoBehaviour
////{
////    [System.Serializable]
////    public class Plank
////    {
////        public GameObject prefab; // Plank prefab
////    }
////    [SerializeField] TMP_Text levelText;
////    public List<Plank> allPlanks = new List<Plank>(); // List of all planks
////    public Transform startPlankPosition;             // Start position for the road
////    public int playerLevel = 1;                      // Current player level
////    public int minPlanks = 5;                        // Minimum number of planks in a road
////    public int maxPlanks = 10;                       // Maximum number of planks in a road
////    public Plank startPlank;                         // First plank of the road
////    public Plank endPlank;                           // Last plank of the road

////    private Transform currentEndTransform;           // Tracks the end position and rotation
////    private Dictionary<GameObject, int> plankCounts; // Tracks the count of each plank prefab

////    public bool canGenerate = false;
////    void Awake()
////    {

////        if (PlayerPrefs.HasKey("PlayerLevel"))
////        {
////            playerLevel = PlayerPrefs.GetInt("PlayerLevel");
////        }
////        else
////        {
////            PlayerPrefs.SetInt("PlayerLevel", playerLevel);
////            PlayerPrefs.Save();
////        }
////        if (PlayerPrefs.HasKey("MaxPlanks"))
////        {
////            maxPlanks = PlayerPrefs.GetInt("MaxPlanks");
////        }
////        else
////        {
////            PlayerPrefs.SetInt("MaxPlanks", maxPlanks);
////            PlayerPrefs.Save();
////        }
////    }
////    private void Start()
////    {

////        levelText.text = "LEVEL " + playerLevel;
////        if (canGenerate)
////        {
////            GenerateRoad();
////        }

////    }


////    private void OnEnable()
////    {
////        ActionHelper.LevelComplete += IncreasePlayerLevel;
////    }

////    private void OnDisable()
////    {
////        ActionHelper.LevelComplete -= IncreasePlayerLevel;
////    }

////    [ContextMenu("Generate")]   
////    public void GenerateRoad()
////    {
////        // Initialize the plank count dictionary
////        plankCounts = new Dictionary<GameObject, int>();

////        // Add all planks from the list
////        foreach (var plank in allPlanks)
////        {
////            if (!plankCounts.ContainsKey(plank.prefab))
////                plankCounts[plank.prefab] = 0;
////        }

////        // Add start and end planks if not already in the dictionary
////        if (startPlank != null && !plankCounts.ContainsKey(startPlank.prefab))
////        {
////            plankCounts[startPlank.prefab] = 0;
////        }

////        if (endPlank != null && !plankCounts.ContainsKey(endPlank.prefab))
////        {
////            plankCounts[endPlank.prefab] = 0;
////        }

////        // Reset position
////        currentEndTransform = new GameObject("CurrentEnd").transform;
////        currentEndTransform.position = startPlankPosition.position;
////        currentEndTransform.rotation = startPlankPosition.rotation;

////        // Clear existing road (if any)
////        foreach (Transform child in transform)
////        {
////            Destroy(child.gameObject);
////        }

////        // Adjust maxPlanks based on player level
////        maxPlanks = Mathf.Clamp(maxPlanks + (playerLevel / 10), minPlanks, 50);

////        // Generate road
////        int plankCount = Random.Range(minPlanks, maxPlanks + 1);

////        // Add the starting plank
////        if (startPlank != null)
////        {
////            PlacePlank(startPlank);
////        }

////        // Generate middle planks based on the player's level
////        for (int i = 0; i < plankCount; i++)
////        {
////            Plank randomPlank = GetRandomPlankForLevel(playerLevel);
////            if (randomPlank != null)
////            {
////                PlacePlank(randomPlank);
////            }
////        }

////        // Add the ending plank
////        if (endPlank != null)
////        {
////            PlacePlank(endPlank);
////        }


////        ActionHelper.SpawnLife?.Invoke();

////        // Cleanup
////        Destroy(currentEndTransform.gameObject);
////    }

////    private void PlacePlank(Plank plank)
////    {
////        // Instantiate the plank at the current end position and rotation
////        GameObject plankObject = Instantiate(plank.prefab, currentEndTransform.position, currentEndTransform.rotation, transform);

////        // Update the count for the placed plank
////        if (plankCounts.ContainsKey(plank.prefab))
////        {
////            plankCounts[plank.prefab]++;
////        }
////        else
////        {
////            Debug.LogError($"Plank prefab {plank.prefab.name} is not in the plankCounts dictionary.");
////            return;
////        }

////        // Find the "End" child transform in the instantiated plank
////        Transform endTransform = plankObject.transform.Find("End");
////        if (endTransform != null)
////        {
////            // Update current end transform to the new end position and rotation
////            currentEndTransform.position = endTransform.position;
////            currentEndTransform.rotation = endTransform.rotation;
////        }
////        else
////        {
////            Debug.LogWarning($"Plank {plank.prefab.name} is missing an 'End' child transform.");
////        }
////    }

////    public void IncreasePlayerLevel()
////    {
////        playerLevel++;
////        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
////        PlayerPrefs.SetInt("MaxPlanks", maxPlanks);


////    }

////    private Plank GetRandomPlankForLevel(int level)
////    {
////        // Determine the range of planks based on level
////        int maxPlankIndex = Mathf.Clamp(level / 10 * 5, 5, allPlanks.Count);

////        // Get a list of planks that haven't exceeded the limit
////        List<Plank> eligiblePlanks = new List<Plank>();

////        for (int i = 0; i < maxPlankIndex; i++)
////        {
////            var plank = allPlanks[i];
////            if (plankCounts.ContainsKey(plank.prefab) && plankCounts[plank.prefab] < 2)
////            {
////                eligiblePlanks.Add(plank);
////            }
////        }

////        if (eligiblePlanks.Count == 0)
////        {
////            Debug.LogWarning("No eligible planks available for placement.");
////            return null;
////        }

////        // Randomly select an eligible plank
////        int randomIndex = Random.Range(0, eligiblePlanks.Count);
////        return eligiblePlanks[randomIndex];
////    }
////}


//////using System.Collections.Generic;
//////using UnityEngine;

//////public class RoadGenerator : MonoBehaviour
//////{
//////    [System.Serializable]
//////    public class Plank
//////    {
//////        public GameObject prefab; // Plank prefab
//////    }

//////    public List<Plank> allPlanks = new List<Plank>(); // List of all planks
//////    public Transform startPlankPosition;             // Start position for the road
//////    public int playerLevel = 1;                      // Current player level
//////    public int minPlanks = 5;                        // Minimum number of planks in a road
//////    public int maxPlanks = 10;                       // Maximum number of planks in a road
//////    public Plank startPlank;                         // First plank of the road
//////    public Plank endPlank;                           // Last plank of the road

//////    private Transform currentEndTransform;           // Tracks the end position and rotation
//////    private Dictionary<GameObject, int> plankCounts; // Tracks the count of each plank prefab

//////    void Start()
//////    {
//////        GenerateRoad();
//////    }

//////    public void GenerateRoad()
//////    {
//////        // Initialize the plank count dictionary
//////        plankCounts = new Dictionary<GameObject, int>();

//////        // Add all planks from the list
//////        foreach (var plank in allPlanks)
//////        {
//////            if (!plankCounts.ContainsKey(plank.prefab))
//////                plankCounts[plank.prefab] = 0;
//////        }

//////        // Add start and end planks if not already in the dictionary
//////        if (startPlank != null && !plankCounts.ContainsKey(startPlank.prefab))
//////        {
//////            plankCounts[startPlank.prefab] = 0;
//////        }

//////        if (endPlank != null && !plankCounts.ContainsKey(endPlank.prefab))
//////        {
//////            plankCounts[endPlank.prefab] = 0;
//////        }

//////        // Reset position
//////        currentEndTransform = new GameObject("CurrentEnd").transform;
//////        currentEndTransform.position = startPlankPosition.position;
//////        currentEndTransform.rotation = startPlankPosition.rotation;

//////        // Clear existing road (if any)
//////        foreach (Transform child in transform)
//////        {
//////            Destroy(child.gameObject);
//////        }

//////        // Generate road
//////        int plankCount = Random.Range(minPlanks, maxPlanks + 1);

//////        // Add the starting plank
//////        if (startPlank != null)
//////        {
//////            PlacePlank(startPlank);
//////        }

//////        // Generate middle planks based on the player's level
//////        for (int i = 0; i < plankCount; i++)
//////        {
//////            Plank randomPlank = GetRandomPlankForLevel(playerLevel);
//////            if (randomPlank != null)
//////            {
//////                PlacePlank(randomPlank);
//////            }
//////        }

//////        // Add the ending plank
//////        if (endPlank != null)
//////        {
//////            PlacePlank(endPlank);
//////        }

//////        ActionHelper.SpawnLife?.Invoke();
//////        // Cleanup
//////        Destroy(currentEndTransform.gameObject);
//////    }

//////    private void PlacePlank(Plank plank)
//////    {
//////        // Instantiate the plank at the current end position and rotation
//////        GameObject plankObject = Instantiate(plank.prefab, currentEndTransform.position, currentEndTransform.rotation, transform);

//////        // Update the count for the placed plank
//////        if (plankCounts.ContainsKey(plank.prefab))
//////        {
//////            plankCounts[plank.prefab]++;
//////        }
//////        else
//////        {
//////            Debug.LogError($"Plank prefab {plank.prefab.name} is not in the plankCounts dictionary.");
//////            return;
//////        }

//////        // Find the "End" child transform in the instantiated plank
//////        Transform endTransform = plankObject.transform.Find("End");
//////        if (endTransform != null)
//////        {
//////            // Update current end transform to the new end position and rotation
//////            currentEndTransform.position = endTransform.position;
//////            currentEndTransform.rotation = endTransform.rotation;
//////        }
//////        else
//////        {
//////            Debug.LogWarning($"Plank {plank.prefab.name} is missing an 'End' child transform.");
//////        }
//////    }

//////    private Plank GetRandomPlankForLevel(int level)
//////    {
//////        // Determine eligible planks based on the player's level
//////        List<Plank> eligiblePlanks = new List<Plank>();

//////        if (level < 10)
//////        {
//////            // Select planks 1 to 5
//////            eligiblePlanks.AddRange(allPlanks.GetRange(0, Mathf.Min(5, allPlanks.Count)));
//////        }
//////        else if (level < 20)
//////        {
//////            // Select planks 1 to 10
//////            eligiblePlanks.AddRange(allPlanks.GetRange(0, Mathf.Min(10, allPlanks.Count)));
//////        }
//////        else
//////        {
//////            // Select all planks
//////            eligiblePlanks.AddRange(allPlanks);
//////        }

//////        // Remove planks that have reached their limit
//////        eligiblePlanks.RemoveAll(plank => plankCounts.ContainsKey(plank.prefab) && plankCounts[plank.prefab] >= 2);

//////        if (eligiblePlanks.Count == 0)
//////        {
//////            Debug.LogWarning("No eligible planks available for placement.");
//////            return null;
//////        }

//////        // Randomly select an eligible plank
//////        int randomIndex = Random.Range(0, eligiblePlanks.Count);
//////        return eligiblePlanks[randomIndex];
//////    }
//////}




//////using System.Collections.Generic;
//////using UnityEngine;

//////public class RoadGenerator : MonoBehaviour
//////{
//////    [System.Serializable]
//////    public class Plank
//////    {
//////        public GameObject prefab; // Plank prefab
//////    }

//////    public List<Plank> allPlanks = new List<Plank>(); // List of all planks
//////    public Transform startPlankPosition;             // Start position for the road
//////    public int playerLevel = 1;                      // Current player level
//////    public int minPlanks = 5;                        // Minimum number of planks in a road
//////    public int maxPlanks = 10;                       // Maximum number of planks in a road
//////    public Plank startPlank;                         // First plank of the road
//////    public Plank endPlank;                           // Last plank of the road

//////    private Transform currentEndTransform;           // Tracks the end position and rotation
//////    private Dictionary<GameObject, int> plankCounts; // Tracks the count of each plank prefab

//////    void Start()
//////    {
//////        GenerateRoad();
//////    }

//////    public void GenerateRoad()
//////    {
//////        // Initialize the plank count dictionary
//////        plankCounts = new Dictionary<GameObject, int>();

//////        // Add all planks from the list
//////        foreach (var plank in allPlanks)
//////        {
//////            if (!plankCounts.ContainsKey(plank.prefab))
//////                plankCounts[plank.prefab] = 0;
//////        }

//////        // Add start and end planks if not already in the dictionary
//////        if (startPlank != null && !plankCounts.ContainsKey(startPlank.prefab))
//////        {
//////            plankCounts[startPlank.prefab] = 0;
//////        }

//////        if (endPlank != null && !plankCounts.ContainsKey(endPlank.prefab))
//////        {
//////            plankCounts[endPlank.prefab] = 0;
//////        }

//////        // Reset position
//////        currentEndTransform = new GameObject("CurrentEnd").transform;
//////        currentEndTransform.position = startPlankPosition.position;
//////        currentEndTransform.rotation = startPlankPosition.rotation;

//////        // Clear existing road (if any)
//////        foreach (Transform child in transform)
//////        {
//////            Destroy(child.gameObject);
//////        }

//////        // Generate road
//////        int plankCount = Random.Range(minPlanks, maxPlanks + 1);

//////        // Add the starting plank
//////        if (startPlank != null)
//////        {
//////            PlacePlank(startPlank);
//////        }

//////        // Generate middle planks based on the player's level
//////        for (int i = 0; i < plankCount; i++)
//////        {
//////            Plank randomPlank = GetRandomPlankForLevel(playerLevel);
//////            if (randomPlank != null)
//////            {
//////                PlacePlank(randomPlank);
//////            }
//////        }

//////        // Add the ending plank
//////        if (endPlank != null)
//////        {
//////            PlacePlank(endPlank);
//////        }

//////        ActionHelper.SpawnLife?.Invoke();
//////        // Cleanup
//////        Destroy(currentEndTransform.gameObject);
//////    }

//////    private void PlacePlank(Plank plank)
//////    {
//////        // Instantiate the plank at the current end position and rotation
//////        GameObject plankObject = Instantiate(plank.prefab, currentEndTransform.position, currentEndTransform.rotation, transform);

//////        // Update the count for the placed plank
//////        if (plankCounts.ContainsKey(plank.prefab))
//////        {
//////            plankCounts[plank.prefab]++;
//////        }
//////        else
//////        {
//////            Debug.LogError($"Plank prefab {plank.prefab.name} is not in the plankCounts dictionary.");
//////            return;
//////        }

//////        // Find the "End" child transform in the instantiated plank
//////        Transform endTransform = plankObject.transform.Find("End");
//////        if (endTransform != null)
//////        {
//////            // Update current end transform to the new end position and rotation
//////            currentEndTransform.position = endTransform.position;
//////            currentEndTransform.rotation = endTransform.rotation;
//////        }
//////        else
//////        {
//////            Debug.LogWarning($"Plank {plank.prefab.name} is missing an 'End' child transform.");
//////        }
//////    }

//////    private Plank GetRandomPlankForLevel(int level)
//////    {
//////        // Get a list of planks that haven't exceeded the limit
//////        List<Plank> eligiblePlanks = new List<Plank>();

//////        foreach (var plank in allPlanks)
//////        {
//////            if (plankCounts.ContainsKey(plank.prefab) && plankCounts[plank.prefab] < 2)
//////            {
//////                eligiblePlanks.Add(plank);
//////            }
//////        }

//////        if (eligiblePlanks.Count == 0)
//////        {
//////            Debug.LogWarning("No eligible planks available for placement.");
//////            return null;
//////        }

//////        // Randomly select an eligible plank
//////        int randomIndex = Random.Range(0, eligiblePlanks.Count);
//////        return eligiblePlanks[randomIndex];
//////    }
//////}
