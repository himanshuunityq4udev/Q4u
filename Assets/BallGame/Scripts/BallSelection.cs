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
    [SerializeField] Transform player;


    private void Awake()
    {
        if (!PlayerPrefs.HasKey("CurrentBall"))
        {
            PlayerPrefs.SetInt("CurrentBall", 0);
        }
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
            priceText.text = ballData.ballPrice[currentDrone-1].ToString();
        }
    }


    public void ChangeBall(int _change) 
    {
        // Update the current drone index
        currentDrone += _change;

        if (currentDrone > player.childCount - 1)
        {
            currentDrone = 0;
        }
        else if (currentDrone < 0)
        {
            currentDrone = player.childCount - 1;
        }

        for (int i = 0; i < player.childCount; i++)
        {
            player.GetChild(i).gameObject.SetActive(i == currentDrone);
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
        for (int i = 0; i < player.childCount; i++)
        {
            //transform.GetChild(i).gameObject.SetActive(i == currentDrone);
            player.GetChild(i).gameObject.SetActive(i == currentDrone);
            
        }
    }

    public void UnlockBall()
    {
        if (ballData.ballPrice[currentDrone] <= MoneyManager.Instance.playerInfo.money)
        {
            PlayerPrefs.SetInt("CurrentBall", currentDrone);
            ballData.unlockedBalls[currentDrone] = true;
            _selectedBall = currentDrone; // Update the selected ball on unlock.
            UpdateUI();
        }
        else
        {
            Debug.Log("Dont have enough coins");
        }
    }
}
