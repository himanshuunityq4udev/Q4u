using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] int revivalAmount = 200;
    private void OnEnable()
    {
        ActionHelper.deductLife += DeductLife;
        ActionHelper.addLife += AddLife;
    }

    private void OnDisable()
    {
        ActionHelper.deductLife -= DeductLife;
        ActionHelper.addLife -= AddLife;
    
    }
    public void AddLife()
    {
        playerData.life++;
    }

    public void DeductLife()
    {
        playerData.life--;
        ActionHelper.updateLifeUI?.Invoke();
    }

    public void ReviveAllLife()
    {
        if (MoneyManager.Instance.playerInfo.money > revivalAmount)
        {
            MoneyManager.Instance.SpendMoney(revivalAmount);
            playerData.life = playerData.totalLife;
            ActionHelper.updateLifeUI?.Invoke();
            ActionHelper.ResetPlayer?.Invoke();
        }
        else
        {
            ActionHelper.DontHaveEnoughCoins?.Invoke();
        }
    }

}
