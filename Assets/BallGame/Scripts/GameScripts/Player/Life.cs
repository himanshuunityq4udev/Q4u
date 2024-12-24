using UnityEngine;

public class Life : MonoBehaviour
{
   [SerializeField] PlayerData playerData;

    public bool isCollected;
    public bool IsCollected => isCollected;

    private void Update()
    {
        if (playerData.life < playerData.totalLife && !isCollected)
        {
            gameObject.GetComponent<SphereCollider>().enabled = true;
            for (int j = 0; j < transform.childCount; j++)
            {
                if (j == PlayerPrefs.GetInt("CurrentBall"))
                {

                    transform.GetChild(j).gameObject.SetActive(true);
                }
            }

        }
        if (playerData.life == playerData.totalLife && (isCollected | !isCollected))
        {
            DisableAllChild(false);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")&& !isCollected)
        {
                isCollected = true;
                ActionHelper.addLife.Invoke();
                playerData.respawnPosition = transform.position;
                DisableAllChild(false);

        }
    }

    private void DisableAllChild(bool value)
    {
        gameObject.GetComponent<SphereCollider>().enabled = false;
        for (int j = 0; j < transform.childCount; j++)
        {
            transform.GetChild(j).gameObject.SetActive(value);
        }
    }
}
