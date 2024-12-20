using UnityEngine;
using UnityEngine.UI;


public class Health : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    private void OnEnable()
    {
        ActionHelper.addLife += AddLife;
    }

    private void OnDisable()
    {

        ActionHelper.addLife -= AddLife;
    }
    public void AddLife()
    {
        playerData.life++;
    }

}
