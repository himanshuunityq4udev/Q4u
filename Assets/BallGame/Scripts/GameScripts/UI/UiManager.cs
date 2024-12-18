using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] MenuController menuController;
    [SerializeField] Page levelCompletePage;
    [SerializeField] Page levelFailedPage;
    [SerializeField] Page dontHaveEnoughCoinPage;


    private void OnEnable()
    {
        ActionHelper.LevelComplete += LevelComplete;
        ActionHelper.LevelFailed += LevelFailed;
        ActionHelper.DontHaveEnoughCoins += DontHaveEnoughCoins;
    }
    private void OnDisable()
    {
        ActionHelper.LevelComplete -= LevelComplete;
        ActionHelper.LevelFailed -= LevelFailed;
        ActionHelper.DontHaveEnoughCoins -= DontHaveEnoughCoins;

    }


    public void LevelComplete()
    {
        menuController.PushPage(levelCompletePage);
    }

    public void LevelFailed()
    {
        menuController.PushPage(levelFailedPage);
    }
    public void DontHaveEnoughCoins()
    {
        menuController.PushPage(dontHaveEnoughCoinPage);
    }


}
