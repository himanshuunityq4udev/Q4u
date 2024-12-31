using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BallSelection : MonoBehaviour
{
    
    [Header("Navigation Buttons")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Select/Unlock Buttons")]
    [SerializeField] private Button selectButton;
    [SerializeField] private Button unlockButton;
    [SerializeField] private TMP_Text priceText;

    [Header("Ball Attributes")]
    private int currentDrone = 0;
    private int _selectedBall;

    [SerializeField] BallData ballData;
    [SerializeField] Transform playerContainer;


    private void Awake()
    {
        if (!PlayerPrefs.HasKey("CurrentBall"))
        {
            PlayerPrefs.SetInt("CurrentBall", 0);
        }
    }

    private void OnEnable()
    {
        ActionHelper.SelectBall += SelectBall;   
        ActionHelper.UnlockBall += UnlockBall;
        ActionHelper.ResetBallSelection += ResetSelectedBall;
    }

    private void OnDisable()
    {
        ActionHelper.SelectBall -= SelectBall;
        ActionHelper.UnlockBall -= UnlockBall;
        ActionHelper.ResetBallSelection -= ResetSelectedBall;



    }

    void Start()
    {
        currentDrone = PlayerPrefs.GetInt("CurrentBall");
        _selectedBall = currentDrone; // Save the currently selected ball.
        SelectBall();
    }

    private void UpdateUI()
    {
        if (ballData.unlockedBalls[currentDrone] == true)
        {
            selectButton.gameObject.SetActive(true);
            unlockButton.gameObject.SetActive(false);
        }
        else // If not, show the buy button and set the price
        {
            selectButton.gameObject.SetActive(false);
            unlockButton.gameObject.SetActive(true);
            priceText.text = ballData.ballPrice[currentDrone].ToString();
        }
    }


    public void ChangeBall(int _change) 
    {
        // Update the current drone index
        currentDrone += _change;

        if (currentDrone > playerContainer.childCount - 1)
        {
            currentDrone = 0;
        }
        else if (currentDrone < 0)
        {
            currentDrone = playerContainer.childCount - 1;
        }

        for (int i = 0; i < playerContainer.childCount; i++)
        {
            playerContainer.GetChild(i).gameObject.SetActive(i == currentDrone);
        }
        UpdateUI();
    }

    public void ResetSelectedBall()
    {
        currentDrone = _selectedBall; // Reset to previously selected ball.
        SelectBall();                 // Update the UI to reflect the reset.
        UpdateUI();                   // Update buttons and price text.
    }

    public void SelectBall()
    {
        for (int i = 0; i < playerContainer.childCount; i++)
        {
            playerContainer.GetChild(i).gameObject.SetActive(i == currentDrone);
            if (i == currentDrone)
            {
                PlayerPrefs.SetInt("CurrentBall", currentDrone);
            }
            
        }
        ActionHelper.updateLifeUI?.Invoke();
    }

    public void UnlockBall()
    {
        if (ballData.ballPrice[currentDrone] <= MoneyManager.Instance.playerInfo.money)
        {
            PlayerPrefs.SetInt("CurrentBall", currentDrone);
            ballData.unlockedBalls[currentDrone] = true;
            _selectedBall = currentDrone; // Update the selected ball on unlock.
            UpdateUI();
            GameObject particle = Instantiate(ballData.unlockedParticle,transform);
            Destroy(particle, 2);
            ActionHelper.updateLifeUI?.Invoke();
        }
        else
        {
            ActionHelper.DontHaveEnoughCoins?.Invoke();
        }
    }
}
