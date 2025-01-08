using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
    [SerializeField] private Button PrivacyPolicyButton;
    [SerializeField] private Button MoreGamesButton;



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
    [SerializeField] Animator levelAnimator;
    [SerializeField] Animator checkpointAnimator;


    private void OnEnable()
    {
        ActionHelper.LevelComplete += LevelComplete;
        ActionHelper.LevelFailed += LevelFailed;
        ActionHelper.DontHaveEnoughCoins += DontHaveEnoughCoins;
        ActionHelper.Skip += Skip;
        ActionHelper.CheckPoint += PlayCheckpointAnimation;
    }
    private void OnDisable()
    {
        ActionHelper.LevelComplete -= LevelComplete;
        ActionHelper.LevelFailed -= LevelFailed;
        ActionHelper.DontHaveEnoughCoins -= DontHaveEnoughCoins;
        ActionHelper.Skip -= Skip;
        ActionHelper.CheckPoint -= PlayCheckpointAnimation;



    }

    private void Start()
    {
        //------ Main Menu---------
        PlayButton.onClick.AddListener(OnPlayButtonClicked);
        BallSelectionButton.onClick.AddListener(OnBallButtonClicked);
        SkySelectionButton.onClick.AddListener(OnWorldButtonClicked);
        // FreeCoinButton.onClick.AddListener(OnFreeCoinsButtonClicked);
        // StoreButton.onClick.AddListener(OnStoreButtonClicked);
        SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
        // ===== End =========

        //------ Settings panel -----
        //PrivacyPolicyButton.onClick.AddListener(OnPrivacyPolicyButtonClicked);
        //MoreGamesButton.onClick.AddListener(OnMoreGamesButtonClicked);

        //------- Ball Selection -----
        BallSelcetButton.onClick.AddListener(OnBallSelectButtonClicked);
        BallUnlockButton.onClick.AddListener(OnBallUnlockButtonClicked);

        //------- Sky Selection -----
        SkySelcetButton.onClick.AddListener(OnSkySelectButtonClicked);
        SkyUnlockButton.onClick.AddListener(OnSkyUnlockButtonClicked);
        BackButton.onClick.AddListener(OnBackButtonClicked);

        //------- add Life --------
        AddLifeButton.onClick.AddListener(OnAddLifeButtonClicked);
        HomeButton1.onClick.AddListener(OnHomeButtonClicked);


        //-------- Revive Life --------
        ReviveLifeButton.onClick.AddListener(OnReviveLifeButtonClicked);
        HomeButton2.onClick.AddListener(OnHomeButtonClicked);

        //------- Next Level -------
        NextButton.onClick.AddListener(OnNextButtonClicked);

    }

    #region -------Main Panel ------------

    private void OnPlayButtonClicked()
    {
        menuController.PushPage(gamePage);
        AddButtonShowOrHide(false);
        CoinButtonShowOrHide(false);
        levelAnimator.SetTrigger("Level");
    }

    private void OnBallButtonClicked()
    {
        menuController.PushPage(ballSelectionPage);
        ballCam.SetActive(true);
        BackButtonShowOrHide(true);
    }

    private void OnWorldButtonClicked()
    {
        menuController.PushPage(skySelectionPage);
        skyCam.SetActive(true);
        BackButtonShowOrHide(true);
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

    public void OnPrivacyPolicyButtonClicked()
    {

    }

    public void OnMoreGamesButtonClicked()
    {

    }
    public void OnMainMenuOpen()
    {
        BackButtonShowOrHide(false);
        AddButtonShowOrHide(true);
        CoinButtonShowOrHide(true);
    }

    #endregion

    #region --------Ball Selection Panel ----------

    private void OnBallSelectButtonClicked()
    {
        menuController.PopPage();
        BackButtonShowOrHide(false);
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
        ActionHelper.ResetBallSelection?.Invoke();
        ActionHelper.ResetSkySelection?.Invoke();
        menuController.PopPage();
        backButton.SetActive(false);
        skyCam.SetActive(false);
        menuController.PopPage();
        backButton.SetActive(false);
        ballCam.SetActive(false);
    }

    #region --------Sky Selection Panel ----------

    private void OnSkySelectButtonClicked()
    {
        menuController.PopPage();
        BackButtonShowOrHide(false);
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
        mainCoinButton.SetActive(true);

    }
    public void OnNextButtonClicked()
    {
        ActionHelper.AnimateCoins?.Invoke();
    }

    private void Skip()
    {
        menuController.PopPage();
        mainCoinButton.SetActive(false);
        levelAnimator.SetTrigger("Level");
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
    private void PlayCheckpointAnimation()
    {
        checkpointAnimator.SetTrigger("CheckPoint");
    }

    private void AddButtonShowOrHide(bool value)
    {
        removeAdsButton.SetActive(value);
    }
    private void CoinButtonShowOrHide(bool value)
    {
        mainCoinButton.SetActive(value);
    }
    private void BackButtonShowOrHide(bool value)
    {
        backButton.SetActive(value);
    }

}
