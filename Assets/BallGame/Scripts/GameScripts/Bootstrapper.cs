using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    public static Bootstrapper Instance { get; private set; }

    [SerializeField] private string initialScene = "MainMenu";

    private void Awake()
    {
        // Ensure a single instance (Singleton pattern)
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeGame();
    }

    private void InitializeGame()
    {
        Debug.Log("Initializing game systems...");

        // Initialize critical systems
        InitializeManagers();

        // Load the initial scene
        LoadInitialScene();
    }

    private void InitializeManagers()
    {
        // Example: Add initialization logic for managers here
        // AudioManager.Instance.Initialize();
        // SaveManager.Instance.LoadData();
    }

    private void LoadInitialScene()
    {
        if (!string.IsNullOrEmpty(initialScene))
        {
            Debug.Log($"Loading initial scene: {initialScene}");
            SceneManager.LoadScene(initialScene);
        }
    }

}
