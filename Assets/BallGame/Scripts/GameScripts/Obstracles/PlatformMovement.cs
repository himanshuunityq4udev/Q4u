using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    /// <summary>
    /// How far the platform can move in either direction from its starting position
    /// </summary>
    public float platformRange = 3;

    /// <summary>
    /// Which direction the platform will move
    /// </summary>
    private float platformDirection = 1;

    /// <summary>
    /// How fast the platform will move
    /// </summary>
    [SerializeField] private float platformStep = 1;

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


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // Determine movement axis based on the active mode
        Vector3 movementAxis;

        if (moveUpDown)
        {
            movementAxis = transform.up; // Vertical movement
        }
        else if (moveForwardBackward)
        {
            movementAxis = transform.forward; // Forward-backward movement
        }
        else
        {
            movementAxis = transform.right; // Left-right movement
        }

        // Check range and change direction if necessary
        if (moveUpDown)
        {
            if (transform.localPosition.y > startingPosition.y + platformRange)
            {
                platformDirection = -1;
            }
            else if (transform.localPosition.y < startingPosition.y - platformRange)
            {
                platformDirection = 1;
            }
        }
        else if (moveForwardBackward)
        {
            if (transform.localPosition.z > startingPosition.z + platformRange)
            {
                platformDirection = -1;
            }
            else if (transform.localPosition.z < startingPosition.z - platformRange)
            {
                platformDirection = 1;
            }
        }
        else
        {
            if (transform.localPosition.x > startingPosition.x + platformRange)
            {
                platformDirection = -1;
            }
            else if (transform.localPosition.x < startingPosition.x - platformRange)
            {
                platformDirection = 1;
            }
        }

        // Apply movement
        transform.Translate(movementAxis * Time.deltaTime * platformStep * platformDirection, Space.World);
    }
}
