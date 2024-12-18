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


    /// ______________UI__________________

    //CollicionHandler script -> UIManager script
    public static Action LevelComplete;

    //Ball script -> UIManager script
    public static Action LevelFailed;


    //Health script -> UImanager script
    public static Action DontHaveEnoughCoins;
}
