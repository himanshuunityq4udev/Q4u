using UnityEngine;

public class Life : MonoBehaviour
{
   [SerializeField] PlayerData playerData;

    public bool isCollected;
    public bool IsCollected => isCollected;

    private void OnEnable()
    {
        ActionHelper.NeedLifeBall += EnablelifeBall;
    }

    private void OnDisable()
    {
        ActionHelper.NeedLifeBall -= EnablelifeBall;
    }

    private void Update()
    {
        if (playerData.life == playerData.totalLife && (isCollected | !isCollected))
        {
            DisableAllChild(false);
        }
    }


    public void EnablelifeBall()
    {
        if(!isCollected)
        {
            for (int j = 0; j < transform.childCount; j++)
            {
                transform.GetChild(j).gameObject.SetActive(j == PlayerPrefs.GetInt("CurrentBall"));
            }
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
        else if(other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            playerData.respawnPosition = transform.position;
            //TODO call animation checkpoint
            ActionHelper.CheckPoint?.Invoke();
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
