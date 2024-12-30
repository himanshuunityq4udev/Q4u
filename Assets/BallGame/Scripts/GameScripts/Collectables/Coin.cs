using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject particles;
    [SerializeField] int coinAmount = 7;
    [SerializeField] int moveSpeed = 50;
    bool isCollected = false;

    private void Update()
    {
        if (!isCollected)
        {
            transform.Rotate(0, 1 * Time.deltaTime * moveSpeed, 0);
        }
    }



    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            isCollected = true;
            GameObject particle =  Instantiate(particles, transform.position, Quaternion.identity);
            ActionHelper.AddNumberOfCoins?.Invoke(coinAmount);
            Destroy(this.gameObject);
            Destroy(particle, 2);
        }
        
    }
}
