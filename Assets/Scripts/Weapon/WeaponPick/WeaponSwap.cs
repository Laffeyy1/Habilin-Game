using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public Transform weaponSlot;
    public GameObject weaponParent;

    private Agent agent; // Reference to the Agent script

    void Start()
    {
        agent = transform.parent.GetComponent<Agent>();  // Get a reference to the Agent script on the same GameObject

        // Instantiate the activeWeapon and set its parent to weaponSlot
        var weapon = Instantiate(weaponParent, weaponSlot.transform.position, weaponSlot.transform.rotation);
        weapon.transform.parent = weaponSlot;

        // Assign the weaponParent component to the Agent's currentWeapon field
        agent.weaponParent = weaponSlot.GetComponentInChildren<WeaponParent>();
    }

    public void UpdateWeapon(GameObject newWeapon)
    {
        weaponParent = newWeapon;

        // Instantiate the newWeapon and set its parent to weaponSlot
        var weapon = Instantiate(weaponParent, weaponSlot.transform.position, weaponSlot.transform.rotation);
        weapon.transform.parent = weaponSlot;

        // Assign the new weaponParent component to the Agent's currentWeapon field
        agent.weaponParent = weapon.GetComponent<WeaponParent>();
    }
}
