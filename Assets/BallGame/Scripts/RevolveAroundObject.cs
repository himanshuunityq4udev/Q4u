using UnityEngine;

public class RevolveAroundObject : MonoBehaviour
{
[Header("Target and Revolution Settings")]
public Transform target; // The object to revolve around
public float revolutionSpeed = 10f; // Speed of revolution in degrees per second
public float radius = 5f; // Radius of revolution
public Vector3 axis = Vector3.up; // Axis of revolution
public float heightOffset = 0f; // Height offset from the target

private float angle;

void Update()
{
    if (target == null)
    {
        Debug.LogWarning("Target is not set. Please assign a target object.");
        return;
    }

    // Increment angle based on speed and time
    angle += revolutionSpeed * Time.deltaTime;

    // Calculate the position around the target
    Vector3 offset = new Vector3(
        Mathf.Sin(angle * Mathf.Deg2Rad) * radius,
        heightOffset,
        Mathf.Cos(angle * Mathf.Deg2Rad) * radius
    );

    // Rotate the offset around the axis
    offset = Quaternion.AngleAxis(angle, axis) * offset;

    // Set the new position
    transform.position = target.position +offset;

    // Make the object look at the target
    transform.LookAt(target);
}
}
