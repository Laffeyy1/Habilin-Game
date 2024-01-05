using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Library Item", menuName = "Library/Item")]
public class LibraryItem : ScriptableObject
{
    public Sprite itemImage;
    public string itemName;
    [TextArea(15, 20)]
    public string description;
}