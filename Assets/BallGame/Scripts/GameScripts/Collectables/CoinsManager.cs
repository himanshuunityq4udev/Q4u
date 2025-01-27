﻿using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class CoinsManager : MonoBehaviour
{
	//References
	[Header ("UI references")]
	[SerializeField] TMP_Text coinUIText;
	[SerializeField] TMP_Text mainCoinUIText;
    [SerializeField] GameObject animatedCoinPrefab;
	[SerializeField] Transform target;
	[SerializeField] Transform spawnPos;

	[Space]
	[Header ("Available coins : (coins to pool)")]
	[SerializeField] int maxCoins;
	Queue<GameObject> coinsQueue = new Queue<GameObject> ();


	[Space]
	[Header ("Animation settings")]
	[SerializeField] [Range (0.5f, 0.9f)] float minAnimDuration;
	[SerializeField] [Range (0.9f, 2f)] float maxAnimDuration;

	[SerializeField] Ease easeType;
	[SerializeField] float spread;

	Vector3 targetPosition;

	private int collectedCoins = 0;


	void Awake ()
	{
		targetPosition = target.position;
		//prepare pool
		PrepareCoins ();
	}


    private void OnEnable()
    {
        ActionHelper.OnMoneyChanged += UpdateMoneyUI; // Subscribe to the event
		ActionHelper.AnimateCoins += AnimateCoins;
		ActionHelper.AddNumberOfCoins += AddCoins;
    }

    private void OnDisable()
    {
        ActionHelper.OnMoneyChanged -= UpdateMoneyUI; // Unsubscribe to avoid memory leaks
        ActionHelper.AnimateCoins -= AnimateCoins;
        ActionHelper.AddNumberOfCoins -= AddCoins;


    }

    private void UpdateMoneyUI(int newAmount)
    {
        mainCoinUIText.text = newAmount.ToString();
    }


    void PrepareCoins ()
	{
		GameObject coin;
		for (int i = 0; i < maxCoins; i++) {
			coin = Instantiate (animatedCoinPrefab);
			coin.transform.parent = transform;
			coin.SetActive (false);
			coinsQueue.Enqueue (coin);

		}
	}

    void Animate (Vector3 collectedCoinPosition, int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			//check if there's coins in the pool
			if (coinsQueue.Count > 0)
			{
				//extract a coin from the pool
				GameObject coin = coinsQueue.Dequeue ();
				coin.SetActive (true);

				//move coin to the collected coin pos
				coin.transform.position = collectedCoinPosition + new Vector3 (Random.Range (-spread, spread), 0f, 0f);

				//animate coin to target position
				float duration = Random.Range (minAnimDuration, maxAnimDuration);
				coin.transform.DOMove (targetPosition, duration)
				.SetEase (easeType)
				.OnComplete (() => {
					//executes whenever coin reach target position
					coin.SetActive (false);
					coinsQueue.Enqueue (coin);
                });
			}
		}
       
    }

	public void AnimateCoins()
	{
        Animate (spawnPos.position,(collectedCoins/7));
		StartCoroutine(GenerateNewLevel());
    }

	IEnumerator GenerateNewLevel()
	{
        MoneyManager.Instance.AddMoney(collectedCoins);
        yield return new WaitForSeconds(1.2f);
		ActionHelper.Skip?.Invoke();

    }


	public void AddCoins (int amount)
	{
		collectedCoins += amount;
        coinUIText.text = collectedCoins.ToString();
        Debug.Log("Himanshu" + collectedCoins);
    }
}
