using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopItemEntry
{
    public GameObject itemPrefab;
    public int cost;
    public float weight; // Add a weight field for each item.
}

[CreateAssetMenu(fileName = "ShopItem", menuName = "Shop/Shop Item")]
public class ShopItem : ScriptableObject
{
    [SerializeField] public List<ShopItemEntry> shopItems = new List<ShopItemEntry>();

    public GameObject GetRandomShopItem()
    {
        if (shopItems.Count == 0)
        {
            Debug.LogError("Shop is empty. Add items to the shop.");
            return null;
        }

        // Calculate the total weight of all items in the shop.
        float totalWeight = 0f;
        foreach (ShopItemEntry entry in shopItems)
        {
            totalWeight += entry.weight;
        }

        // Generate a random value within the total weight range.
        float randomValue = Random.Range(0f, totalWeight);

        // Select a shop item based on its weight.
        foreach (ShopItemEntry entry in shopItems)
        {
            if (randomValue < entry.weight)
            {
                return entry.itemPrefab;
            }
            randomValue -= entry.weight;
        }

        // Fallback if something goes wrong.
        Debug.LogWarning("Unable to select a random shop item. Returning null.");
        return null;
    }

    public int GetShopItemCost(GameObject itemPrefab)
    {
        foreach (ShopItemEntry entry in shopItems)
        {
            if (entry.itemPrefab == itemPrefab)
            {
                return entry.cost;
            }
        }

        Debug.LogWarning("Item not found in the shop. Returning cost of 0.");
        return 0;
    }
}
