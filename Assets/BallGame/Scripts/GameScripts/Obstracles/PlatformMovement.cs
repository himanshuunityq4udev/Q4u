using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Make sure to import DOTween

public class PlatformMovement : MonoBehaviour
{
    /// <summary>
    /// How far the platform can move in either direction from its starting position
    /// </summary>
    public float platformRange = 3;

    /// <summary>
    /// How fast the platform will move
    /// </summary>
    [SerializeField] private float platformStep = 1.5f;

    /// <summary>
    /// Where the platform starts
    /// </summary>
    private Vector3 startingPosition;

    /// <summary>
    /// If true, the platform will move forward and backward instead of left and right
    /// </summary>
    public bool moveForwardBackward = false;

    /// <summary>
    /// If true, the platform will move up and down
    /// </summary>
    public bool moveUpDown = false;

    /// <summary>
    /// The ease type for the platform movement
    /// </summary>
    public Ease platformEase = Ease.InOutSine;

    /// <summary>
    /// The loop type for the platform movement
    /// </summary>
    public LoopType platformLoopType = LoopType.Yoyo;

    /// <summary>
    /// Delay in seconds after reaching each endpoint
    /// </summary>
    public float platformDelay = 1f;

    private Tween platformTween;

    void Start()
    {
        startingPosition = transform.localPosition;
        StartMovement();
    }

    private void StartMovement()
    {
        // Determine the movement axis and the target position based on the mode
        Vector3 movementAxis;

        if (moveUpDown)
        {
            movementAxis = Vector3.up;
        }
        else if (moveForwardBackward)
        {
            movementAxis = Vector3.forward;
        }
        else
        {
            movementAxis = Vector3.right;
        }

        Vector3 targetPosition1 = startingPosition + movementAxis * platformRange;
        Vector3 targetPosition2 = startingPosition - movementAxis * platformRange;

        // Create a looping tween with delays between movements
        platformTween = transform.DOLocalMove(targetPosition1, platformRange / platformStep)
            .SetEase(platformEase)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(platformDelay, () =>
                {
                    transform.DOLocalMove(targetPosition2, platformRange / platformStep)
                        .SetEase(platformEase)
                        .OnComplete(() =>
                        {
                            DOVirtual.DelayedCall(platformDelay, StartMovement);
                        });
                });
            });
    }

    private void OnDestroy()
    {
        // Clean up the tween when the object is destroyed
        if (platformTween != null && platformTween.IsActive())
        {
            platformTween.Kill();
        }
    }
}
