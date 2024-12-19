using UnityEngine;

public class Life : MonoBehaviour
{
   [SerializeField] PlayerData playerData;

    private bool isCollected;
    public bool IsCollected => isCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                isCollected = true;
                ActionHelper.addLife.Invoke();
                playerData.respawnPosition = transform.position;
                gameObject.SetActive(false);
        }
    }
}
