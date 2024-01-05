using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void ButtonMenu()
    {
        audioManager.playMainMenuBGM();
    }
    public void ButtonAccept()
    {
        audioManager.ButtonAccept();
    }
    public void ButtonDecline()
    {
        audioManager.ButtonDecline();
    }
    public void ButtonBuy()
    {
        audioManager.ButtonBuy();
    }
    public void ButtonPause()
    {
        audioManager.ButtonPause();
    }
    public void ButtonUnpause()
    {
        audioManager.ButtonUnpause();
    }
    public void AttackSFX()
    {
        audioManager.playAttack();
    }
    public void GetHit()
    {
        audioManager.GetHit();
    }
    public void Portal()
    {
        audioManager.Portal();
    }
}
