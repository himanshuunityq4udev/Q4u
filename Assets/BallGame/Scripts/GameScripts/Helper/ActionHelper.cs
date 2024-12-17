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



}
