using UnityEngine;
using System;

public class ActionHelper : MonoBehaviour
{
    //PlayerInput Script -> Ball Script
    public static Action<Vector2, float, float> onMove;

    //Ball Script -> Health Script
    public static Action deductLife;


    //Life Script -> Health Script,BallsLifeManager
    public static Action addLife;

    //HealthScript ->ballLifeManager script
    public static Action updateLifeUI;

    //HealthScript ->ball script
    public static Action ResetPlayer;

    //LevelGenerator -> lifeSpawnManager,ball Script
    public static Action SpawnLife;


    //UiManager script -> LevelLoader script
    public static Action GoHome;

    /// ______________UI__________________

    //CollicionHandler script -> UIManager script,coinManager Script
    public static Action LevelComplete;

    //Ball script -> UIManager script
    public static Action LevelFailed;


    //Health script -> UImanager script
    //UIManager Script -> BallSelection Script
    //UIManager Script -> SkyBoxChange Script
    public static Action DontHaveEnoughCoins;

    //MonwyManager script -> CoinManager script
    public static  Action<int> OnMoneyChanged; // Event to notify money changes

    //UImanaget script  -> BallSelection Script
    public static Action SelectBall;
    public static Action UnlockBall;
    public static Action ResetBallSelection;

    //UImanaget script  -> SkySelection Script
    public static Action SelectSky;
    public static Action UnlockSky;
    public static Action ResetSkySelection;

    //UIManager Script ->Health Script
    public static Action ReviveLife;
}
