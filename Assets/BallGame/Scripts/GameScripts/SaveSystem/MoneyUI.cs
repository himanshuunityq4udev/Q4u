using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    private void OnEnable()
    {
        ActionHelper.OnMoneyChanged += UpdateMoneyUI; // Subscribe to the event
    }

    private void OnDisable()
    {
        ActionHelper.OnMoneyChanged -= UpdateMoneyUI; // Unsubscribe to avoid memory leaks
    }

    private void UpdateMoneyUI(int newMoney)
    {
        moneyText.text = $"Money: {newMoney}";
    }
}
