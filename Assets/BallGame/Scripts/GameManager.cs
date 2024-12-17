using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
       Time.timeScale = 1;     
    }


    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
    }
}
