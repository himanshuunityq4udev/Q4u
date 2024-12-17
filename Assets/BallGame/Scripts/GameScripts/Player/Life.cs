using UnityEngine;

public class Life : MonoBehaviour
{
   [SerializeField] PlayerData playerData;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                ActionHelper.addLife.Invoke();
                playerData.respawnPosition = transform.position;
                Destroy(gameObject);
        }
    }
}
