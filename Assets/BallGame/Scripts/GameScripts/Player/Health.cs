using UnityEngine;
using UnityEngine.UI;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    public Image[] balls;
    public Sprite fullBall;
    public Sprite emptyBall;
    public Transform StartPos;

    private Vector3 respanPosition;

    public  int health;
    public  int numberOfBalls;

    private void Start()
    {
        health = playerData.life;
        numberOfBalls = playerData.totalLife;
        UpdateBallImage();
        SetRespanPosition(StartPos.position);
    }

    private void OnEnable()
    {
        ActionHelper.deductLife += DeductLife;
        ActionHelper.addLife += AddLife;
    }

    private void OnDisable()
    {
        ActionHelper.deductLife -= DeductLife;
        ActionHelper.addLife -= AddLife;
    }


    public void AddLife()
    {
        playerData.life += 1;
       // health = playerData.life;
    }

    public void DeductLife()
    {
        playerData.life -= 1;
        //health = playerData.life;
    }

    public void UpdateBallLife(int val)
    {
       // health += val;

        //playerData.life += val;
        Invoke("UpdateBallImage", .85f);
    }
    public void SetRespanPosition(Vector3 position)
    {
        respanPosition = position;
       
    }

    public Vector3 GetRespanPosition()
    {
        return respanPosition;
    }


    // Update is called once per frame
    void UpdateBallImage()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            if(health > numberOfBalls)
            {
                health = numberOfBalls;
            }


            if (i < health)
            {
                balls[i].sprite = fullBall;
            }
            else
            {
                balls[i].sprite = emptyBall;
            }
            if (i < numberOfBalls)
            {
                balls[i].enabled = true;
            }
            else
            {
                balls[i].enabled = false;
            }
        }
    }

   
}
