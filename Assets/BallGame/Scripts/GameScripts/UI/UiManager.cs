using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{

    [SerializeField] MenuController menuController;
    [SerializeField] Page levelCompletePage;
    [SerializeField] Page levelFailedPage;
    [SerializeField] Page dontHaveEnoughCoinPage;
    [SerializeField] Page gamePage;
    [SerializeField] Page ballSelectionPage;
    [SerializeField] Page skySelectionPage;
    [SerializeField] Page settingsPage;
    [SerializeField] Page unlockBallsPage;

    [Header("Buttons")]
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button BallSelectionButton;
    [SerializeField] private Button SkySelectionButton;
    [SerializeField] private Button FreeCoinButton;
    [SerializeField] private Button StoreButton;

    //SelectButton BallSelection Page
    [SerializeField] private Button BallSelcetButton;
    [SerializeField] private Button BallUnlockButton;
    //SelectSky SkySelection Page
    [SerializeField] private Button SkySelcetButton;
    [SerializeField] private Button SkyUnlockButton;
    [SerializeField] private Button BackButton;

    //Level Failed Page
    [SerializeField] private Button AddLifeButton;
    [SerializeField] private Button HomeButton1;

    //Level Failed Page
    [SerializeField] private Button ReviveLifeButton;
    [SerializeField] private Button HomeButton2;

    //LevelComplete
    [SerializeField] private Button NextButton;


    [SerializeField] private PlayerData playerData;


    [Header("Buttons GameObject")]
    [SerializeField] GameObject mainCoinButton;
    [SerializeField] GameObject backButton;
    [SerializeField] GameObject removeAdsButton;


    [SerializeField] GameObject ballCam;
    [SerializeField] GameObject skyCam;

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

    private void Start()
    {
        //------Main Menu---------
        PlayButton.onClick.AddListener(OnPlayButtonClicked);
        BallSelectionButton.onClick.AddListener(OnBallButtonClicked);
        SkySelectionButton.onClick.AddListener(OnWorldButtonClicked);
        // FreeCoinButton.onClick.AddListener(OnFreeCoinsButtonClicked);
        // StoreButton.onClick.AddListener(OnStoreButtonClicked);
        SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
        // ===== End =========

        //-------Ball Selection -----
        BallSelcetButton.onClick.AddListener(OnBallSelectButtonClicked);
        BallUnlockButton.onClick.AddListener(OnBallUnlockButtonClicked);

        //-------Sky Selection -----
        SkySelcetButton.onClick.AddListener(OnSkySelectButtonClicked);
        SkyUnlockButton.onClick.AddListener(OnSkyUnlockButtonClicked);
        BackButton.onClick.AddListener(OnBackButtonClicked);

        //------- add Life --------
        AddLifeButton.onClick.AddListener(OnAddLifeButtonClicked);
        HomeButton1.onClick.AddListener(OnHomeButtonClicked);


        //--------Revive Life --------
        ReviveLifeButton.onClick.AddListener(OnReviveLifeButtonClicked);
        HomeButton2.onClick.AddListener(OnHomeButtonClicked);

        //Next Level
        NextButton.onClick.AddListener(OnNextButtonClicked);

    }

    #region -------Main Panel ------------

    private void OnPlayButtonClicked()
    {
        menuController.PushPage(gamePage);
        removeAdsButton.SetActive(false);
        mainCoinButton.SetActive(false);
    }

    private void OnBallButtonClicked()
    {
        menuController.PushPage(ballSelectionPage);
        ballCam.SetActive(true);
        backButton.SetActive(true);
    }

    private void OnWorldButtonClicked()
    {
        menuController.PushPage(skySelectionPage);
        skyCam.SetActive(true);
        backButton.SetActive(true);
    }

    public void OnSettingsButtonClicked()
    {
        menuController.PushPage(settingsPage);
    }

    public void OnFreeCoinsButtonClicked()
    {

    }

    public void OnStoreButtonClicked()
    {

    }

    public void OnMainMenuOpen()
    {
        backButton.SetActive(false);
        removeAdsButton.SetActive(true);
        mainCoinButton.SetActive(true);
    }

    #endregion

    #region --------Ball Selection Panel ----------

    private void OnBallSelectButtonClicked()
    {
        menuController.PopPage();
        backButton.SetActive(false);
        ballCam.SetActive(false);
        ActionHelper.SelectBall?.Invoke();
    }
    private void OnBallUnlockButtonClicked()
    {
        ActionHelper.UnlockBall?.Invoke();

    }


    #endregion

    private void OnBackButtonClicked()
    {
        OnBallSelectButtonClicked();
        ActionHelper.ResetBallSelection?.Invoke();
        OnSkySelectButtonClicked();

    }

    #region --------Sky Selection Panel ----------

    private void OnSkySelectButtonClicked()
    {
        menuController.PopPage();
        backButton.SetActive(false);
        skyCam.SetActive(false);
        ActionHelper.SelectSky?.Invoke();
    }
    private void OnSkyUnlockButtonClicked()
    {
        ActionHelper.UnlockSky?.Invoke();

    }
    #endregion


    private void LevelComplete()
    {
        menuController.PushPage(levelCompletePage);
    }
    public void OnNextButtonClicked()
    {

        ActionHelper.GenerateNewLevel?.Invoke();
        menuController.PopPage();
    }


    #region------ Level Failed Panel -------------

    private void LevelFailed()
    {
        menuController.PushPage(levelFailedPage);
    }

    private void OnAddLifeButtonClicked()
    {
        menuController.PopPage();
        menuController.PushPage(unlockBallsPage);
    }
    #endregion

    #region------ Unlock Balls panel  -------------

    private void OnReviveLifeButtonClicked()
    {
        menuController.PopPage();
        ActionHelper.ReviveLife?.Invoke();
    }
    #endregion

    private void OnHomeButtonClicked()
    {
        ActionHelper.GoHome?.Invoke();
    }

    private void DontHaveEnoughCoins()
    {
        menuController.PushPage(dontHaveEnoughCoinPage);
    }


}
