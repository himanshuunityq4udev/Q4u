
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class BallsLifeManager : MonoBehaviour
{
    //References
    [Header("UI references")]
    [SerializeField] GameObject animatedBallPrefab;
    [SerializeField] Transform spawnPos;

    [Space]
    [Header("Available Balls : (Balls to pool)")]
    [SerializeField] private int maxBalls = 10;
    Queue<GameObject> ballsQueue = new Queue<GameObject>();


    [Space]
    [Header("Animation settings")]
    [SerializeField][Range(0.5f, 0.9f)] private float minAnimDuration = 0.5f;
    [SerializeField][Range(0.9f, 2f)] private float maxAnimDuration = 1.5f;

 [SerializeField] private Ease easeType = Ease.InOutQuad;
    [SerializeField] float spread;

    public Image[] balls;
    public Sprite fullBall;
    public Sprite emptyBall;
    //public Transform StartPos;
    [SerializeField] private PlayerData playerData;

    Vector3 targetPosition;


    void Awake()
    {
        targetPosition = FindFirstEmptyBallTransform()?.position ?? Vector3.zero;
        //prepare pool
        PrepareBalls();
       
    }

    private void Start()
    {
        UpdateBallImage();
    }

    private void OnEnable()
    {
        ActionHelper.addLife += AddLife;
        ActionHelper.updateLifeUI += UpdateBallImage;
    }
    private void OnDisable()
    {
       ActionHelper.addLife -= AddLife;
        ActionHelper.updateLifeUI -= UpdateBallImage;
    }

    /// <summary>
    /// Prepares the object pool for animating balls.
    /// </summary>
    void PrepareBalls()
    {
        for (int i = 0; i < maxBalls; i++)
        {
            GameObject ball = Instantiate(animatedBallPrefab, transform);
            ball.SetActive(false);
            ballsQueue.Enqueue(ball);
        }
    }
    /// <summary>
    /// Handles animating balls to the first empty ball's position.
    /// </summary>
    /// <param name="collectedBallPosition">Spawn position of the animated ball.</param>
    /// <param name="amount">Number of balls to animate.</param>
    void Animate(Vector3 collectedBallPosition, int amount)
    {
       int animationsCompleted = 0; // Counter to track completed animations

        Vector3 dynamicTargetPosition = FindFirstEmptyBallTransform()?.position ?? targetPosition;


        for (int i = 0; i < amount; i++)
        {

            //check if there's coins in the pool
            if (ballsQueue.Count > 0)
            {
                //extract a coin from the pool
                GameObject ball = ballsQueue.Dequeue();
                ball.SetActive(true);

                //move coin to the collected coin pos
                ball.transform.position = collectedBallPosition;

                //animate coin to target position
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                ball.transform.DOMove(dynamicTargetPosition, duration)
                .SetEase(easeType)
                .OnComplete(() => {
                    //executes whenever coin reach target position
                    ball.SetActive(false);
                    ballsQueue.Enqueue(ball);
                    animationsCompleted++;
                    if (animationsCompleted == amount)
                    {
                        UpdateBallImage();
                    }
                });
            }
        }
    }

    /// <summary>
    /// Adds life to the player and animates it.
    /// </summary>
    public void AddLife()
    {
        Animate(spawnPos.position, 1);
    }

      /// <summary>
    /// Finds the transform of the first empty ball.
    /// </summary>
    /// <returns>The transform of the first empty ball or null if all are full.</returns>
    Transform FindFirstEmptyBallTransform()
    {
        foreach (var ball in balls)
        {
            if (ball.sprite == emptyBall)
                return ball.transform;
        }
        return null;
    }


    /// <summary>
    /// Updates the ball images to reflect the current life status.
    /// </summary>
    void UpdateBallImage()
    {
        playerData.life = Mathf.Clamp(playerData.life, 0, playerData.totalLife);

        for (int i = 0; i < balls.Length; i++)
        {
            if (i < playerData.life)
            {
                balls[i].sprite = fullBall;
                balls[i].enabled = true;
            }
            else if (i < playerData.totalLife)
            {
                balls[i].sprite = emptyBall;
                balls[i].enabled = true;
            }
            else
            {
                balls[i].enabled = false;
            }
        }
    }
}
