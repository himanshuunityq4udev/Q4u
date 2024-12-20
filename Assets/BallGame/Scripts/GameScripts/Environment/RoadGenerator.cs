using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoadGenerator : MonoBehaviour
{
    [System.Serializable]
    public class Plank
    {
        public GameObject prefab; // Plank prefab
    }
    [SerializeField] TMP_Text levelText;
    public List<Plank> allPlanks = new List<Plank>(); // List of all planks
    public Transform startPlankPosition;             // Start position for the road
    public int playerLevel = 1;                      // Current player level
    public int minPlanks = 5;                        // Minimum number of planks in a road
    public int maxPlanks = 10;                       // Maximum number of planks in a road
    public Plank startPlank;                         // First plank of the road
    public Plank endPlank;                           // Last plank of the road

    private Transform currentEndTransform;           // Tracks the end position and rotation
    private Dictionary<GameObject, int> plankCounts; // Tracks the count of each plank prefab

    void Start()
    {

        //if (PlayerPrefs.HasKey("PlayerLevel"))
        //{
        //    playerLevel = PlayerPrefs.GetInt("PlayerLevel");
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("PlayerLevel", playerLevel);
        //    PlayerPrefs.Save();
        //}
        //if (PlayerPrefs.HasKey("MaxPlanks"))
        //{
        //    maxPlanks = PlayerPrefs.GetInt("MaxPlanks");
        //}
        //else
        //{
        //    PlayerPrefs.SetInt("MaxPlanks", maxPlanks);
        //    PlayerPrefs.Save();
        //}

        //levelText.text = "LEVEL " + playerLevel; ;
        GenerateRoad();

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
        // Initialize the plank count dictionary
        plankCounts = new Dictionary<GameObject, int>();

        // Add all planks from the list
        foreach (var plank in allPlanks)
        {
            if (!plankCounts.ContainsKey(plank.prefab))
                plankCounts[plank.prefab] = 0;
        }

        // Add start and end planks if not already in the dictionary
        if (startPlank != null && !plankCounts.ContainsKey(startPlank.prefab))
        {
            plankCounts[startPlank.prefab] = 0;
        }

        if (endPlank != null && !plankCounts.ContainsKey(endPlank.prefab))
        {
            plankCounts[endPlank.prefab] = 0;
        }

        // Reset position
        currentEndTransform = new GameObject("CurrentEnd").transform;
        currentEndTransform.position = startPlankPosition.position;
        currentEndTransform.rotation = startPlankPosition.rotation;

        // Clear existing road (if any)
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Adjust maxPlanks based on player level
        maxPlanks = Mathf.Clamp(maxPlanks + (playerLevel / 10), minPlanks, 50);

        // Generate road
        int plankCount = Random.Range(minPlanks, maxPlanks + 1);

        // Add the starting plank
        if (startPlank != null)
        {
            PlacePlank(startPlank);
        }

        // Generate middle planks based on the player's level
        for (int i = 0; i < plankCount; i++)
        {
            Plank randomPlank = GetRandomPlankForLevel(playerLevel);
            if (randomPlank != null)
            {
                PlacePlank(randomPlank);
            }
        }

        // Add the ending plank
        if (endPlank != null)
        {
            PlacePlank(endPlank);
        }

        
        ActionHelper.SpawnLife?.Invoke();
        
        // Cleanup
        Destroy(currentEndTransform.gameObject);
    }

    private void PlacePlank(Plank plank)
    {
        // Instantiate the plank at the current end position and rotation
        GameObject plankObject = Instantiate(plank.prefab, currentEndTransform.position, currentEndTransform.rotation, transform);

        // Update the count for the placed plank
        if (plankCounts.ContainsKey(plank.prefab))
        {
            plankCounts[plank.prefab]++;
        }
        else
        {
            Debug.LogError($"Plank prefab {plank.prefab.name} is not in the plankCounts dictionary.");
            return;
        }

        // Find the "End" child transform in the instantiated plank
        Transform endTransform = plankObject.transform.Find("End");
        if (endTransform != null)
        {
            // Update current end transform to the new end position and rotation
            currentEndTransform.position = endTransform.position;
            currentEndTransform.rotation = endTransform.rotation;
        }
        else
        {
            Debug.LogWarning($"Plank {plank.prefab.name} is missing an 'End' child transform.");
        }
    }

    public void IncreasePlayerLevel()
    {
        playerLevel++;
        PlayerPrefs.SetInt("PlayerLevel", playerLevel);
        PlayerPrefs.SetInt("MaxPlanks", maxPlanks);


    }

    private Plank GetRandomPlankForLevel(int level)
    {
        // Determine the range of planks based on level
        int maxPlankIndex = Mathf.Clamp(level / 10 * 5, 5, allPlanks.Count);

        // Get a list of planks that haven't exceeded the limit
        List<Plank> eligiblePlanks = new List<Plank>();

        for (int i = 0; i < maxPlankIndex; i++)
        {
            var plank = allPlanks[i];
            if (plankCounts.ContainsKey(plank.prefab) && plankCounts[plank.prefab] < 2)
            {
                eligiblePlanks.Add(plank);
            }
        }

        if (eligiblePlanks.Count == 0)
        {
            Debug.LogWarning("No eligible planks available for placement.");
            return null;
        }

        // Randomly select an eligible plank
        int randomIndex = Random.Range(0, eligiblePlanks.Count);
        return eligiblePlanks[randomIndex];
    }
}


//using System.Collections.Generic;
//using UnityEngine;

//public class RoadGenerator : MonoBehaviour
//{
//    [System.Serializable]
//    public class Plank
//    {
//        public GameObject prefab; // Plank prefab
//    }

//    public List<Plank> allPlanks = new List<Plank>(); // List of all planks
//    public Transform startPlankPosition;             // Start position for the road
//    public int playerLevel = 1;                      // Current player level
//    public int minPlanks = 5;                        // Minimum number of planks in a road
//    public int maxPlanks = 10;                       // Maximum number of planks in a road
//    public Plank startPlank;                         // First plank of the road
//    public Plank endPlank;                           // Last plank of the road

//    private Transform currentEndTransform;           // Tracks the end position and rotation
//    private Dictionary<GameObject, int> plankCounts; // Tracks the count of each plank prefab

//    void Start()
//    {
//        GenerateRoad();
//    }

//    public void GenerateRoad()
//    {
//        // Initialize the plank count dictionary
//        plankCounts = new Dictionary<GameObject, int>();

//        // Add all planks from the list
//        foreach (var plank in allPlanks)
//        {
//            if (!plankCounts.ContainsKey(plank.prefab))
//                plankCounts[plank.prefab] = 0;
//        }

//        // Add start and end planks if not already in the dictionary
//        if (startPlank != null && !plankCounts.ContainsKey(startPlank.prefab))
//        {
//            plankCounts[startPlank.prefab] = 0;
//        }

//        if (endPlank != null && !plankCounts.ContainsKey(endPlank.prefab))
//        {
//            plankCounts[endPlank.prefab] = 0;
//        }

//        // Reset position
//        currentEndTransform = new GameObject("CurrentEnd").transform;
//        currentEndTransform.position = startPlankPosition.position;
//        currentEndTransform.rotation = startPlankPosition.rotation;

//        // Clear existing road (if any)
//        foreach (Transform child in transform)
//        {
//            Destroy(child.gameObject);
//        }

//        // Generate road
//        int plankCount = Random.Range(minPlanks, maxPlanks + 1);

//        // Add the starting plank
//        if (startPlank != null)
//        {
//            PlacePlank(startPlank);
//        }

//        // Generate middle planks based on the player's level
//        for (int i = 0; i < plankCount; i++)
//        {
//            Plank randomPlank = GetRandomPlankForLevel(playerLevel);
//            if (randomPlank != null)
//            {
//                PlacePlank(randomPlank);
//            }
//        }

//        // Add the ending plank
//        if (endPlank != null)
//        {
//            PlacePlank(endPlank);
//        }

//        ActionHelper.SpawnLife?.Invoke();
//        // Cleanup
//        Destroy(currentEndTransform.gameObject);
//    }

//    private void PlacePlank(Plank plank)
//    {
//        // Instantiate the plank at the current end position and rotation
//        GameObject plankObject = Instantiate(plank.prefab, currentEndTransform.position, currentEndTransform.rotation, transform);

//        // Update the count for the placed plank
//        if (plankCounts.ContainsKey(plank.prefab))
//        {
//            plankCounts[plank.prefab]++;
//        }
//        else
//        {
//            Debug.LogError($"Plank prefab {plank.prefab.name} is not in the plankCounts dictionary.");
//            return;
//        }

//        // Find the "End" child transform in the instantiated plank
//        Transform endTransform = plankObject.transform.Find("End");
//        if (endTransform != null)
//        {
//            // Update current end transform to the new end position and rotation
//            currentEndTransform.position = endTransform.position;
//            currentEndTransform.rotation = endTransform.rotation;
//        }
//        else
//        {
//            Debug.LogWarning($"Plank {plank.prefab.name} is missing an 'End' child transform.");
//        }
//    }

//    private Plank GetRandomPlankForLevel(int level)
//    {
//        // Determine eligible planks based on the player's level
//        List<Plank> eligiblePlanks = new List<Plank>();

//        if (level < 10)
//        {
//            // Select planks 1 to 5
//            eligiblePlanks.AddRange(allPlanks.GetRange(0, Mathf.Min(5, allPlanks.Count)));
//        }
//        else if (level < 20)
//        {
//            // Select planks 1 to 10
//            eligiblePlanks.AddRange(allPlanks.GetRange(0, Mathf.Min(10, allPlanks.Count)));
//        }
//        else
//        {
//            // Select all planks
//            eligiblePlanks.AddRange(allPlanks);
//        }

//        // Remove planks that have reached their limit
//        eligiblePlanks.RemoveAll(plank => plankCounts.ContainsKey(plank.prefab) && plankCounts[plank.prefab] >= 2);

//        if (eligiblePlanks.Count == 0)
//        {
//            Debug.LogWarning("No eligible planks available for placement.");
//            return null;
//        }

//        // Randomly select an eligible plank
//        int randomIndex = Random.Range(0, eligiblePlanks.Count);
//        return eligiblePlanks[randomIndex];
//    }
//}




//using System.Collections.Generic;
//using UnityEngine;

//public class RoadGenerator : MonoBehaviour
//{
//    [System.Serializable]
//    public class Plank
//    {
//        public GameObject prefab; // Plank prefab
//    }

//    public List<Plank> allPlanks = new List<Plank>(); // List of all planks
//    public Transform startPlankPosition;             // Start position for the road
//    public int playerLevel = 1;                      // Current player level
//    public int minPlanks = 5;                        // Minimum number of planks in a road
//    public int maxPlanks = 10;                       // Maximum number of planks in a road
//    public Plank startPlank;                         // First plank of the road
//    public Plank endPlank;                           // Last plank of the road

//    private Transform currentEndTransform;           // Tracks the end position and rotation
//    private Dictionary<GameObject, int> plankCounts; // Tracks the count of each plank prefab

//    void Start()
//    {
//        GenerateRoad();
//    }

//    public void GenerateRoad()
//    {
//        // Initialize the plank count dictionary
//        plankCounts = new Dictionary<GameObject, int>();

//        // Add all planks from the list
//        foreach (var plank in allPlanks)
//        {
//            if (!plankCounts.ContainsKey(plank.prefab))
//                plankCounts[plank.prefab] = 0;
//        }

//        // Add start and end planks if not already in the dictionary
//        if (startPlank != null && !plankCounts.ContainsKey(startPlank.prefab))
//        {
//            plankCounts[startPlank.prefab] = 0;
//        }

//        if (endPlank != null && !plankCounts.ContainsKey(endPlank.prefab))
//        {
//            plankCounts[endPlank.prefab] = 0;
//        }

//        // Reset position
//        currentEndTransform = new GameObject("CurrentEnd").transform;
//        currentEndTransform.position = startPlankPosition.position;
//        currentEndTransform.rotation = startPlankPosition.rotation;

//        // Clear existing road (if any)
//        foreach (Transform child in transform)
//        {
//            Destroy(child.gameObject);
//        }

//        // Generate road
//        int plankCount = Random.Range(minPlanks, maxPlanks + 1);

//        // Add the starting plank
//        if (startPlank != null)
//        {
//            PlacePlank(startPlank);
//        }

//        // Generate middle planks based on the player's level
//        for (int i = 0; i < plankCount; i++)
//        {
//            Plank randomPlank = GetRandomPlankForLevel(playerLevel);
//            if (randomPlank != null)
//            {
//                PlacePlank(randomPlank);
//            }
//        }

//        // Add the ending plank
//        if (endPlank != null)
//        {
//            PlacePlank(endPlank);
//        }

//        ActionHelper.SpawnLife?.Invoke();
//        // Cleanup
//        Destroy(currentEndTransform.gameObject);
//    }

//    private void PlacePlank(Plank plank)
//    {
//        // Instantiate the plank at the current end position and rotation
//        GameObject plankObject = Instantiate(plank.prefab, currentEndTransform.position, currentEndTransform.rotation, transform);

//        // Update the count for the placed plank
//        if (plankCounts.ContainsKey(plank.prefab))
//        {
//            plankCounts[plank.prefab]++;
//        }
//        else
//        {
//            Debug.LogError($"Plank prefab {plank.prefab.name} is not in the plankCounts dictionary.");
//            return;
//        }

//        // Find the "End" child transform in the instantiated plank
//        Transform endTransform = plankObject.transform.Find("End");
//        if (endTransform != null)
//        {
//            // Update current end transform to the new end position and rotation
//            currentEndTransform.position = endTransform.position;
//            currentEndTransform.rotation = endTransform.rotation;
//        }
//        else
//        {
//            Debug.LogWarning($"Plank {plank.prefab.name} is missing an 'End' child transform.");
//        }
//    }

//    private Plank GetRandomPlankForLevel(int level)
//    {
//        // Get a list of planks that haven't exceeded the limit
//        List<Plank> eligiblePlanks = new List<Plank>();

//        foreach (var plank in allPlanks)
//        {
//            if (plankCounts.ContainsKey(plank.prefab) && plankCounts[plank.prefab] < 2)
//            {
//                eligiblePlanks.Add(plank);
//            }
//        }

//        if (eligiblePlanks.Count == 0)
//        {
//            Debug.LogWarning("No eligible planks available for placement.");
//            return null;
//        }

//        // Randomly select an eligible plank
//        int randomIndex = Random.Range(0, eligiblePlanks.Count);
//        return eligiblePlanks[randomIndex];
//    }
//}
