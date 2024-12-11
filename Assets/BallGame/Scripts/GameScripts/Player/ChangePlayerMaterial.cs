using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerMaterial : MonoBehaviour
{
    [SerializeField]
    private int ChangeType = 0;

    [SerializeField] BallsLifeManager coinsManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // other.GetComponent <BallTriggerController>().SetBallType(ChangeType);
            if (other.GetComponent<Health>().health < 4)
            {

                coinsManager.AddCoins(1);

                other.GetComponent<Health>().UpdateBallLife(1);
                other.GetComponent<Health>().SetRespanPosition(transform.position);

                // Debug.Log("Call" + other.GetComponent<BallTriggerController>() != null);
                gameObject.SetActive(false);
            }else
            {
                gameObject.SetActive(false);
            }
        }
    }
    bool activateNow;
    void ActivateBall()
    {
        Debug.Log("call");
        activateNow = true;
    }
}
