using UnityEngine;
using TMPro;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    private GameState currentState;

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private TMP_Text scoreText;

    private int score;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        ChangeState(GameState.MainMenu);
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
        UpdateUI();
    }

    private void UpdateUI()
    {
        mainMenu.SetActive(currentState == GameState.MainMenu);
        pauseMenu.SetActive(currentState == GameState.Paused);
        gameOverScreen.SetActive(currentState == GameState.GameOver);
    }

    public void StartGame()
    {
        score = 0;
        ChangeState(GameState.Playing);
    }

    public void PauseGame()
    {
        ChangeState(GameState.Paused);
    }

    public void ResumeGame()
    {
        ChangeState(GameState.Playing);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
    }
}



public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}