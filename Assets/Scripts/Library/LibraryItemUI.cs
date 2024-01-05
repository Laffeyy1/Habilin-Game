using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LibraryItemUI : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public Image itemImage;

    public void SetItemDetails(LibraryItem item)
    {
        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.description;
        // Set itemImage.sprite to the unlocked item's image.
    }
}
