using System;
using System.Threading.Tasks;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public PlayerInformation playerInfo;
    private const string PlayerDataKey = "players";

    public static event Action<int> OnMoneyChanged; // Event to notify money changes

    public static MoneyManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }

        Instance = this;
    }


    private async void Start()
    { 
        await LoadDataOnSceneChange();
    }

    public async Task LoadDataOnSceneChange()
    {
        try
        {
            playerInfo = await DataSaver.LoadDataAsync<PlayerInformation>(PlayerDataKey);
            if (playerInfo == null)
            {
                Debug.Log("No data found. Initializing new player data.");
                playerInfo = new PlayerInformation { money = 500 }; // Default data
                await SaveDataAsync();
            }
            else
            {
                 NotifyMoneyChanged();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading data: {e.Message}");
        }
    }

    public async Task SaveDataAsync()
    {
        try
        {
            bool saveSuccess = await DataSaver.SaveDataAsync(playerInfo, PlayerDataKey);
            if (saveSuccess)
            {
                Debug.Log("Data saved successfully.");
            }
            else
            {
                Debug.LogWarning("Failed to save data.");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving data: {e.Message}");
        }
    }

    [ContextMenu("Add money")]
    public void AddCoins()
    {
        AddMoney(500);
    }


    [ContextMenu("Spend money")]
    public void SpendCoins()
    {
        SpendMoney(500);
    }

    public void AddMoney(int newAmount)
    {
        if (playerInfo == null) return;

        playerInfo.money += newAmount;
       // Debug.Log($"Money updated to {playerInfo.money}");
        _ = SaveDataAsync(); // Fire and forget
        NotifyMoneyChanged();
    }

    public void SpendMoney(int spendAmount)
    {
        if (playerInfo == null || playerInfo.money < spendAmount)
        {
            Debug.LogWarning("Insufficient funds or uninitialized player data.");
            return;
        }

        playerInfo.money -= spendAmount;

      
      //  Debug.Log($"Money updated to {playerInfo.money}");
        _ = SaveDataAsync(); // Fire and forget
        NotifyMoneyChanged();
    }

    private void NotifyMoneyChanged()
    {
        OnMoneyChanged?.Invoke(playerInfo.money); // Notify subscribers
    }

}
