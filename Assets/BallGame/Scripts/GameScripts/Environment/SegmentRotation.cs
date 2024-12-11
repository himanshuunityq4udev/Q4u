using UnityEngine;

public class SegmentRotation : MonoBehaviour
{

    public int index;

    // Enum to define possible rotation directions for the segment
    public enum RotationDirection
    {
        Forward, // Rotate in the forward direction
        Right,   // Rotate to the right
        Left,    // Rotate to the left
        Custom   // Allow custom rotation
    }

    public RotationDirection rotationDirection = RotationDirection.Forward;  // Default direction
    public Vector3 customRotationAxis = Vector3.up; // Default rotation axis (e.g., up-axis)

    // This function will be called to set the rotation of the next segment
    public void SetNextSegmentRotation(Transform lastEndPoint)
    {
        if (lastEndPoint != null)
        {
            Vector3 direction = lastEndPoint.position - transform.position;

            Quaternion targetRotation = Quaternion.identity;

            // Apply rotation based on the selected direction
            switch (rotationDirection)
            {
                case RotationDirection.Forward:
                    targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                    break;

                case RotationDirection.Right:
                    targetRotation = Quaternion.LookRotation(Vector3.Cross(direction, Vector3.up), Vector3.up);
                    break;

                case RotationDirection.Left:
                    targetRotation = Quaternion.LookRotation(Vector3.Cross(Vector3.up, direction), Vector3.up);
                    break;

                case RotationDirection.Custom:
                    targetRotation = Quaternion.Euler(customRotationAxis);  // Apply custom rotation if defined
                    break;
            }

            // Apply the calculated rotation to this segment
            transform.rotation = targetRotation;
        }
    }
}
