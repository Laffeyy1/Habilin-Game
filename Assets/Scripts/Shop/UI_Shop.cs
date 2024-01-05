using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Shop : MonoBehaviour
{
    [SerializeField] private ShopItem shopItemSO;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private int numberOfItemsToDisplay = 4;
    [SerializeField] private Button rerollButton;
    [SerializeField] private TMP_Text rerollPrice;
    [SerializeField] private Transform container;
    GameObject shopUI;
    int cost = 20;

    private void Awake()
    {
        shopUI = GameObject.FindGameObjectWithTag("Shop");
        rerollButton.onClick.AddListener(() => RerollItems(cost));
        CreateRandomItemButtons();
    }

    private void CreateRandomItemButtons()
    {
        List<ShopItemEntry> itemsToDisplay = GetRandomShopItems(numberOfItemsToDisplay);

        foreach (ShopItemEntry entry in itemsToDisplay)
        {
            // Create a UI button for each shop item.
            GameObject shopItemButton = Instantiate(buttonPrefab, container);
            RectTransform shopItemRectTransform = shopItemButton.GetComponent<RectTransform>();

            // Access the UI elements in your button (e.g., Image and Text components).
            Image itemImage = shopItemButton.transform.Find("ItemImage").GetComponent<Image>();
            TextMeshProUGUI itemNameText = shopItemButton.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI itemCostText = shopItemButton.transform.Find("ItemCost").GetComponent<TextMeshProUGUI>();

            // Set the UI elements based on the shop item's information.
            itemImage.sprite = entry.itemPrefab.GetComponent<SpriteRenderer>().sprite; // Assuming you use SpriteRenderer for items.
            itemNameText.text = entry.itemPrefab.name; // Set the item's name.
            itemCostText.text = entry.cost.ToString(); // Set the item's cost.

            Button button = shopItemButton.GetComponent<Button>();
            button.onClick.AddListener(() => OnShopItemButtonClick(entry.itemPrefab, entry.cost));
        }
    }

    private List<ShopItemEntry> GetRandomShopItems(int count)
    {
        List<ShopItemEntry> randomItems = new List<ShopItemEntry>();
        List<ShopItemEntry> availableItems = new List<ShopItemEntry>(shopItemSO.shopItems);

        while (randomItems.Count < count && availableItems.Count > 0)
        {
            // Calculate the total weight of all available items.
            float totalWeight = 0f;
            foreach (ShopItemEntry entry in availableItems)
            {
                totalWeight += entry.weight;
            }

            // Generate a random value within the total weight range.
            float randomValue = Random.Range(0f, totalWeight);

            // Select a shop item based on its weight.
            foreach (ShopItemEntry entry in availableItems)
            {
                if (randomValue < entry.weight)
                {
                    randomItems.Add(entry);
                    availableItems.Remove(entry);
                    break;
                }
                randomValue -= entry.weight;
            }
        }

        return randomItems;
    }

    private void OnShopItemButtonClick(GameObject itemPrefab, int cost)
    {
        if (itemPrefab == null)
        {
            Debug.LogWarning("Item prefab is null.");
            return;
        }
        if (CoinUI.instance.TryDeductCoins(cost))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                Vector3 playerPosition = player.transform.position;
                Instantiate(itemPrefab, playerPosition, Quaternion.identity);
                shopUI.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Player not found. Cannot instantiate item.");
            }
        }
    }

    private void RerollItems(int price)
    {
        if (CoinUI.instance.TryDeductCoins(price))
        {
            cost *= 2;
            rerollPrice.text = cost.ToString();

            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }

            // Create new random item buttons.
            CreateRandomItemButtons();
        }
        else
        {
            rerollPrice.text = "<color=red> Not enough coins";
            rerollButton.interactable = false;
        }

    }
}