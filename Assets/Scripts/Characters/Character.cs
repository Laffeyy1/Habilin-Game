using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Characters")]
public class Character : ScriptableObject
{
    public int ID;

    public string characterName;
    public string description;

    public Sprite sprite;
    public Sprite displayIcon;

    public int health;
    public int speed;
    public int mana;
}
