using UnityEngine;

public class Life : MonoBehaviour
{
   [SerializeField] PlayerData playerData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                // BallsLifeManager.addLife?.Invoke();
                ActionHelper.addLife.Invoke();
                other.GetComponent<Health>().SetRespanPosition(transform.position);
                Destroy(gameObject);
        }
    }
}
