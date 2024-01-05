using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinUI : MonoBehaviour, IDataPersistence
{
    public int coinCount = 0; // Changed to a static variable.

    public TextMeshProUGUI coinCountText;

    public static CoinUI instance;

    public int testlapu;
    private void Awake()
    {
        // Assign the instance to this object when it's created.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            // If an instance already exists, destroy this one.
            Destroy(gameObject);
        }
    }
    public void LoadData(GameData data)
    {
        coinCount = data.coins;
    }

    public void SaveData(GameData data)
    {
        data.coins = coinCount;
    }
    public bool TryDeductCoins(int amount)
    {
        // Check if the player has enough coins to deduct.
        if (coinCount >= amount)
        {
            coinCount -= amount;
            return true; // Deduction successful.
        }
        else
        {
            Debug.LogWarning("Not enough coins!");
            return false; // Deduction failed due to insufficient coins.
        }
    }

    private void Update()
    {
        coinCountText.text = "" + coinCount;
    }
}
