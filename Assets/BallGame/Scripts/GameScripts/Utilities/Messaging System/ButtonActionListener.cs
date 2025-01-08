using UnityEngine;

public class ButtonActionListener : MonoBehaviour
{
    private void OnEnable()
    {
        // Subscribe to the OnButtonClicked event
        ButtonActionManager.OnButtonClicked += HandleButtonAction;
    }

    private void OnDisable()
    {
        // Unsubscribe to avoid memory leaks
        ButtonActionManager.OnButtonClicked -= HandleButtonAction;
    }

    private void HandleButtonAction(ButtonsName buttonName)
    {
        // Handle logic based on the button name
        switch (buttonName)
        {
            case ButtonsName.PlayButton:
                Debug.Log("Play button clicked! Starting game...");
                StartGame();
                break;

            case ButtonsName.SettingsButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;


            case ButtonsName.CoinsButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

            case ButtonsName.RemovAdsButton:
                Debug.Log("Play button clicked! Starting game...");
                StartGame();
                break;

            case ButtonsName.BallSelectionButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;


            case ButtonsName.WorldSelectionButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

            case ButtonsName.StoreButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

            case ButtonsName.GetFreeCoinsButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

               // Other Buttons---------------
            case ButtonsName.BackButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

            case ButtonsName.HomeButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

                //Settings Panel---------------------
            case ButtonsName.CloseButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;
            case ButtonsName.PrivacyPolicyButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

            case ButtonsName.MoreGamesButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

                //[EnumHeader("Ball Selection Panel")]
            case ButtonsName.SelectBallButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

            case ButtonsName.UnlockBallButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

            case ButtonsName.SelectSkyButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

                //[EnumHeader("Sky Selection Panel")]
            case ButtonsName.UnlockSkyButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

                //[EnumHeader("Level Failed Panel")]
            case ButtonsName.AddLifeButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

                //[EnumHeader("Level Complete Panel")]
            case ButtonsName.RewardedAdsButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;
            case ButtonsName.SkipButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;
            case ButtonsName.NextLevelButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;
                //[EnumHeader("Not Enough Coins Panel")]
            case ButtonsName.BuyCoinButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;

            //[EnumHeader("Revive Panel")]
            case ButtonsName.ReviveLifeButton:
                Debug.Log("Exit button clicked! Exiting game...");
                ExitGame();
                break;
        }
    }

    private void StartGame()
    {
        // Example logic for starting the game
        Debug.Log("Game Started!");
    }

    private void ExitGame()
    {
        // Example logic for exiting the game
        Debug.Log("Game Exited!");
        Application.Quit();
    }
}
