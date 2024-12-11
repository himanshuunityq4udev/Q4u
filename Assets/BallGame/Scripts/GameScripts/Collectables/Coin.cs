using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject particles;

    private void OnTriggerEnter(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            Instantiate(particles, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
