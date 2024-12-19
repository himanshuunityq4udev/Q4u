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

    //HealthScript -> ball script
    public static Action ResetPlayer;

    //RoadGenerator -> lifeSpawnManager Script
    public static Action SpawnLife;


    //UiManager script -> LevelLoader script
    public static Action GoHome;


    //DimandCollector script - coinmanager script
    public static Action<int> AddNumberOfCoins;

    /// ______________UI__________________

    //CollicionHandler script -> UIManager script,coinManager Script, RoadGenerator script
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

    //UImanager script  -> RoadGenerator script, CollisionHandler script
    public static Action GenerateNewLevel;
}
