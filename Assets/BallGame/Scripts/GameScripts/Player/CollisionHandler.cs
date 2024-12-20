using UnityEngine;
using CandyCoded.HapticFeedback;
public class CollisionHandler : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            HapticFeedback.MediumFeedback();
        }
    }
}
