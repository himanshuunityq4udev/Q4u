using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    private void OnEnable()
    {
        MoneyManager.OnMoneyChanged += UpdateMoneyUI; // Subscribe to the event
    }

    private void OnDisable()
    {
        MoneyManager.OnMoneyChanged -= UpdateMoneyUI; // Unsubscribe to avoid memory leaks
    }

    private void UpdateMoneyUI(int newMoney)
    {
        moneyText.text = $"Money: {newMoney}";
    }
}
