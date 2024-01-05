using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int amount = 1;
    private bool collected = false;
    private bool subscribedToEvent = false;
    AudioManager audioManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collected)
        {
            HandleCoinCollection();
        }
    }

    private void HandleCoinCollection()
    {
        if (!collected)
        {
            collected = true;
            // Trigger the event when the coin is collected.
            GameEventsManager.instance.CoinCollected();
        }
    }

    // Subscribe to the event when this component is enabled.
    private void OnEnable()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (!subscribedToEvent)
        {
            GameEventsManager.instance.onCoinCollected += OnPlayerCollect;
            subscribedToEvent = true;
        }
    }

    // Unsubscribe from the event when this component is disabled.
    private void OnDisable()
    {
        if (subscribedToEvent)
        {
            GameEventsManager.instance.onCoinCollected -= OnPlayerCollect;
            subscribedToEvent = false;
        }
    }

    public void OnPlayerCollect()
    {
        if (!collected)
        {
            CoinUI coinUI = FindObjectOfType<CoinUI>(); // Find the CoinUI object in the scene
            if (coinUI != null)
            {
                coinUI.coinCount += amount; // Increment by the coin's amount
                audioManager.ButtonBuy();
            }
            collected = true;
        }
    }
}
