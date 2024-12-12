using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBallManager : MonoBehaviour
{


    [SerializeField] BallsLifeManager ballLifemanager;

    private void Start()
    {
        ballLifemanager = FindAnyObjectByType<BallsLifeManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<Health>().health < 4)
            {
                ballLifemanager.AddCoins(1);
                other.GetComponent<Health>().UpdateBallLife(1);
                other.GetComponent<Health>().SetRespanPosition(transform.position);
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
