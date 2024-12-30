using UnityEngine;

public class Life : MonoBehaviour
{
   [SerializeField] PlayerData playerData;
   [SerializeField] Animator checkPointAnimator;

    public bool isCollected;
    public bool IsCollected => isCollected;

    private void Start()
    {
        checkPointAnimator = GetComponentInParent<Animator>();
    }


    private void Update()
    {
        if (playerData.life < playerData.totalLife && !isCollected)
        {
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
        if (other.CompareTag("Player")&& !isCollected && playerData.life < playerData.totalLife)
        {
                isCollected = true;
                ActionHelper.addLife.Invoke();
                playerData.respawnPosition = transform.position;
                DisableAllChild(false);

        }
        else
        {
            isCollected = true;
            playerData.respawnPosition = transform.position;
            checkPointAnimator.enabled = true;
        }
    }

    private void DisableAllChild(bool value)
    {
        for (int j = 0; j < transform.childCount; j++)
        {
            transform.GetChild(j).gameObject.SetActive(value);

        }
    }
}
