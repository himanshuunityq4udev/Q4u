using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DroneSelection : MonoBehaviour
{
    [SerializeField] GameEvent updateUnlockButton;

    [Header("Navigation Buttons")]
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    [Header("Play/Buy Buttons")]
    [SerializeField] private Button play;
    [SerializeField] private Button buy;
    [SerializeField] private GameObject lockImage;
    [SerializeField] private TMP_Text priceText;

    [Header("Car Attributes")]
    private int currentDrone = 0;

    [SerializeField] GameEvent changeSliderProperty;
  
    void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentDrone"))
        {
            PlayerPrefs.SetInt("CurrentDrone", 0);
        }
        currentDrone = PlayerPrefs.GetInt("CurrentDrone");
        SelectDrone(currentDrone);
    }

    private void OnEnable()
    {
        updateUnlockButton.AddListener(UpdateUI);
    }
    private void OnDisable()
    {
        updateUnlockButton.RemoveListener(UpdateUI);
    }

    private void SelectDrone(int _index)
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == _index);
        }
        PlayerPrefs.SetInt("CurrentDrone", currentDrone);
        Debug.Log("Himanshu current" + PlayerPrefs.GetInt("CurrentDrone"));
        UpdateUI();
        changeSliderProperty.Invoke();
    }

    private void UpdateUI()
    {
        if (currentDrone == 0)
        {
            play.gameObject.SetActive(true);
            buy.gameObject.SetActive(false);
        }
        else // If not, show the buy button and set the price
        {
            play.gameObject.SetActive(false);
            buy.gameObject.SetActive(true);
            //priceText.text = MoneyManager.Instance.DronePrices[currentDrone].ToString();
        }
    }


    public void ChangeDrone(int _change) 
    {
        // Update the current drone index
        currentDrone += _change;

        if (currentDrone > transform.childCount - 1)
        {
            currentDrone = 0;
        }
        else if (currentDrone < 0)
        {
            currentDrone = transform.childCount - 1;
        }
        SelectDrone(currentDrone);
    }
}
