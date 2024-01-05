using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootTableEntry
{
    public GameObject itemPrefab;
    public int weight;
}

[CreateAssetMenu(fileName = "New Loot Table", menuName = "LootTable")]
public class LootTable : ScriptableObject
{
    [SerializeField] private List<LootTableEntry> lootEntries = new List<LootTableEntry>();

    public GameObject GetRandomLootItem()
    {
        if (lootEntries.Count == 0)
        {
            Debug.LogError("Loot table is empty. Add items to the loot table.");
            return null;
        }

        int totalWeight = 0;
        foreach (LootTableEntry entry in lootEntries)
        {
            totalWeight += entry.weight;
        }

        int randomValue = Random.Range(0, totalWeight);

        foreach (LootTableEntry entry in lootEntries)
        {
            if (randomValue < entry.weight)
            {
                return entry.itemPrefab;
            }

            randomValue -= entry.weight;
        }

        // This should not happen if you set up the weights correctly, but handle it just in case.
        Debug.LogWarning("Loot table logic error. Returning null.");
        return null;
    }
}