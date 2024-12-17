using UnityEngine;
using CandyCoded.HapticFeedback;
public class CollisionHandler : MonoBehaviour
{
    Rigidbody _ballrb;
    private void Start()
    {
        _ballrb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Complete");
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _ballrb.angularVelocity = Vector3.zero;
            _ballrb.linearVelocity = Vector3.zero;

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            HapticFeedback.MediumFeedback();
        }
    }
}
