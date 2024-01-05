using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weaponToGive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<WeaponSwap>().UpdateWeapon(weaponToGive);
            Destroy(GameObject.FindGameObjectWithTag("Weapon"));
            Destroy(gameObject);
        }
    }
}