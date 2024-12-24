using UnityEngine;

public class CollisionHandler : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground"))
        {
            ActionHelper.PlayHaptic?.Invoke();
        }
    }
}
