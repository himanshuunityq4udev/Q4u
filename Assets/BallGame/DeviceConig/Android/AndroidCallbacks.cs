using UnityEngine;
using UnityEngine.InputSystem;


public class AndroidCallbacks : MonoBehaviour
{
    #region UnityCallBacks
    [SerializeField] bool Test;
    [SerializeField] GameObject moregames;
    public int FreeMoney;
    [SerializeField] MenuController menuController;
    [SerializeField] GameEvent GPGSSigning;

    int tapcount = 0;
    float lastTap = 0;

    private void Awake()
    {
        UnitySplashOver(); //Provide Callback to Android when splash Is over
    }
    private void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        GetFreeCoins();
    }

    private void Update()
    {
        CloseApplication();
    }
    #endregion


    public void UnitySplashOver()
    {
    #if UNITY_ANDROID
            try
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                            {
                                unityActivity.Call("UnitySplashOver");
                            }));
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
    #endif
    }

    public void OpenStorePage()
    {
    #if UNITY_ANDROID
                try
                {
                    AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                    unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        unityActivity.Call("openStorePage", "MainMenu Panel", MoneyManager.Instance.playerInfo.money);
                    }));
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
    #endif
    }

    public void RateGame()
    {
    #if UNITY_ANDROID
                try
                {
                    AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                    unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        unityActivity.Call("rateGame");
                    }));
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
    #endif
    }

    public void OpenPrivacyPolicy()
    {
#if UNITY_ANDROID
                try
                {
                    AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                    unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        unityActivity.Call("openPrivacyPolicy");
                    }));
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
#endif
    }

    public void MoreGames()
    {
    #if UNITY_ANDROID
            try
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    unityActivity.Call("moreGames");
                }));
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

    #endif
    }

    public void ShowServerValue()
    {
    #if UNITY_ANDROID
            try
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    unityActivity.Call("ShowServerValue");
                }));
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

    #endif
    }

    public void AddPageAnalytics(string pageName)
    {
        Debug.Log("Himanshu analytics" + pageName);

    #if UNITY_ANDROID
            try
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    unityActivity.Call("AddPageAnalytics", pageName);
                }));
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

    #endif
    }

    public void GetFreeCoins()
    {
        Debug.Log("Himanshu Get First Coins");

#if UNITY_ANDROID
        try
        {
            AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
            {
                unityActivity.Call("GetFreeCoins"); //----------------------------
            }));
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        if (Test)
        {
            //FirstInstallCoins("2000");
        }
#endif
    }

    public void HideAndShowBannerAds(int index)
    {
    #if UNITY_ANDROID
                bool value;
                if (index == 0)
                {
                    value = false;
                }
                else
                {
                    value = true;
                }
                try
                {
                    AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                    unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        unityActivity.Call("HideBannerAd", value); //----------------------------
                    }));
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }

    #endif
    }

    public void IsUserPremium()
    {
    #if UNITY_ANDROID
                try
                {
                    AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                    unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                    {
                        unityActivity.Call("IsUserPremium"); //----------------------------
                    }));
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }

                if (Test)
                {
                    //ShowMoreGames("true");
                }
    #endif
    }

    public void ShowRewardedAds(string pageName)
    {

    #if UNITY_ANDROID
            try
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    unityActivity.Call("ShowRewarded", pageName); //----------------------------
                }));
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

    #endif
    }

    public void ShareGame()
    {
    #if UNITY_ANDROID
            try
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                    unityActivity.Call("shareGame");
                }));
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
    #endif
    }

    public void ShowFullAds(bool value)
    {
    #if UNITY_ANDROID
            try
            {
                AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject unityActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                unityActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
                {
                   // unityActivity.Call("callFullAd", "MenuPanel", value); //----------------------------
                }));
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }

    #endif
    }




    #region From Android to Unity CallBacks

    public void AdsClosed()
    {
        Debug.Log("Himahsu AdsClosed function call");
        if (Time.timeScale != 0)
        {
            // RefrenceManager.Instance.Resume();
        }
    }



    public void AddCoins(string coinsValue)
    {
        Debug.Log("Himahsu addcoins function call" + coinsValue);
        //PlayerPrefs.SetString("FirstTimeCoins", "Done");
        int coin = int.Parse(coinsValue);
        MoneyManager.Instance.AddMoney(coin);
    }

    public static string premiumValue = "true";
    public void ShowPremiumIcon(string val)
    {
        premiumValue = val;
    }

    public static string userPremiumStatus = "true";
    public void UserPremiumStatus(string val)
    {
        userPremiumStatus = val;
    }


    public void RewardEarned(string adswatchStatus)
    {

        if (adswatchStatus == "true")
        {
            // FuelSystem.Instance.AddFuel(35);
            menuController.PopPage();
            Time.timeScale = 1;
        }
    }

    #endregion

    //Quit the application when click twice on backButton
    private void CloseApplication()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            // Check if this tap is within the allowed time window (0.3 seconds)
            if (Time.time - lastTap <= 0.5f)
            {
                tapcount++;
            }
            else
            {
                tapcount = 1; // Reset to 1 if too much time has passed
            }

            lastTap = Time.time; // Update lastTap time

            if (tapcount >= 2)
            {
                Application.Quit();
            }
        }
    }




    public void FirstInstallCoins(string coins)
    {
        Debug.Log("Himanshu add free coins get from android" + coins);
        int _coin = int.Parse(coins);
        FreeMoney = _coin;
    }

    //public void ShowMoreGames(string value)
    //{
    //    if (value == "true" && moregames != null)
    //    {
    //        moregames.SetActive(true);

    //    }
    //    else
    //    {
    //        moregames.SetActive(false);
    //    }
    //}

}
