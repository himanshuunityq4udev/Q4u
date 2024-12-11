using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondController : MonoBehaviour
{

    //[SerializeField]
    //private float _speed = 10.0f;


    //[SerializeField] GameObject coinNumPrefab;

    [SerializeField]
    private AudioClip _collectClip;

    [SerializeField]
    private ParticleSystem _diamondEffect;

    [SerializeField] CoinsManager coinsManager;


    private void Start()
    {
        coinsManager = FindAnyObjectByType<CoinsManager>();
    }
    //void Update()
    //{
    //    transform.Rotate(Vector3.up * _speed * Time.deltaTime);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //other.GetComponent<PlayerAttributeManager>().SetScore(100);

            //Instantiate(_diamondEffect,transform.position,Quaternion.identity);

            //AudioManager.instance.AudioPlay(_collectClip);
            coinsManager.AddCoins(7);

            
            //Show (+7) number
            //Destroy(Instantiate(coinNumPrefab, other.transform.position, Quaternion.identity), 1f);
            Destroy(this.gameObject);
        }
    }

}
