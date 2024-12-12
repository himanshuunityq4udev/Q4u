using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numberOfBalls;

    public Image[] balls;
    public Sprite fullBall;
    public Sprite emptyBall;
    public Transform StartPos;

    private Vector3 respanPosition;
 

    private void Start()
    {
        UpdateBallImage();
        SetRespanPosition(StartPos.position);
    }


    public void UpdateBallLife(int val)
    {
        health += val;
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
