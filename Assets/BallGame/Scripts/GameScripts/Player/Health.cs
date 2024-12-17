using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

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
        playerData.life += 1;
    }

    public void DeductLife()
    {
        playerData.life -= 1;
        ActionHelper.updateLifeUI?.Invoke();
    }
   
}
