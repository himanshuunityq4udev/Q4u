using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject particles;
    [SerializeField] int coinAmount = 7;

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
           GameObject particle =  Instantiate(particles, transform.position, Quaternion.identity);
            ActionHelper.AddNumberOfCoins?.Invoke(coinAmount);
            Destroy(this.gameObject);
            Destroy(particle, 2);
        }
        
    }
}
