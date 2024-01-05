using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaRestore : MonoBehaviour
{
    [SerializeField]
    private int manaToRestore = 1;
    AudioManager audioManager;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AbilityHolder mana = other.GetComponent<AbilityHolder>();

            if (mana != null)
            {
                mana.RestoreMana(manaToRestore);
                audioManager.RestoreMana();
            }
        }
    }
}
