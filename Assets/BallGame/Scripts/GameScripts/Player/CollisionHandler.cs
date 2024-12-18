using UnityEngine;
using CandyCoded.HapticFeedback;
public class CollisionHandler : MonoBehaviour
{
    Rigidbody _ballrb;
    PlayerInput playerInput;
    [SerializeField] private GameObject cam1;
    [SerializeField] private GameObject cam2;

    private void Start()
    {
        _ballrb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            Debug.Log("Complete");
            ActionHelper.LevelComplete?.Invoke();
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _ballrb.angularVelocity = Vector3.zero;
            _ballrb.linearVelocity = Vector3.zero;
            playerInput.enabled = false;
            cam1.SetActive(false);
            cam2.SetActive(true);
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
