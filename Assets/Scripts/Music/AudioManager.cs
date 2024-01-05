using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---------- Audio Source ----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSrouce;

    [Header("---------- Audio Clip Background ----------")]
    public AudioClip mainMenuBGM;
    public AudioClip tutorialBGM;
    public AudioClip bgm1;
    public AudioClip bgm2;
    public AudioClip bgm3;
    public AudioClip bgm4;
    public AudioClip bgm5;

    [Header("---------- Audio Clip SFX ----------")]
    public AudioClip buttonAccept;
    public AudioClip buttonDecline;
    public AudioClip dialogue;
    public AudioClip buttonBuy;
    public AudioClip skill1Lapu;
    public AudioClip skill2Lapu;
    public AudioClip skill3Lapu;
    public AudioClip skill1Andres;
    public AudioClip skill2Andres;
    public AudioClip buttonPause;
    public AudioClip buttonUnpause;
    public AudioClip coin;
    public AudioClip attack;
    public AudioClip getHit;
    public AudioClip heal;
    public AudioClip mana;
    public AudioClip destroyObject;
    public AudioClip portal;
    public AudioClip chestOpen;

    #region Keep Audio Manager
    private  static AudioManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    private void Start()
    {
        Debug.Log("Start method called.");
        musicSource.clip = mainMenuBGM;
        musicSource.Play();
    }
    public void SetBackgroundClip(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }
    public void SetSFXClip(AudioClip clip)
    {
        SFXSrouce.clip = clip;
        SFXSrouce.Play();
    }
    public void playMainMenuBGM()
    {
        musicSource.clip = mainMenuBGM;
        musicSource.Play();
    }
    public void playTurorialBGM()
    {
        musicSource.clip = tutorialBGM;
        musicSource.Play();
    }

    public void ButtonAccept()
    {
        SFXSrouce.clip = buttonAccept;
        SFXSrouce.Play();
    }
    public void ButtonDecline()
    {
        SFXSrouce.clip = buttonDecline;
        SFXSrouce.Play();
    }
    public void ButtonBuy()
    {
        SFXSrouce.clip = buttonBuy;
        SFXSrouce.Play();
    }
    public void Skill1Lapu()
    {
        SFXSrouce.clip = skill1Lapu;
        SFXSrouce.Play();
    }
    public void Skill2Lapu()
    {
        SFXSrouce.clip = skill2Lapu;
        SFXSrouce.Play();
    }
    public void Skill3Lapu()
    {
        SFXSrouce.clip = skill3Lapu;
        SFXSrouce.Play();
    }
    public void Skill1Andres()
    {
        SFXSrouce.clip = skill1Andres;
        SFXSrouce.Play();
    }
    public void Skill2Andres()
    {
        SFXSrouce.clip = skill2Andres;
        SFXSrouce.Play();
    }
    public void ButtonPause()
    {
        SFXSrouce.clip = buttonPause;
        SFXSrouce.Play();
    }
    public void ButtonUnpause()
    {
        SFXSrouce.clip = buttonUnpause;
        SFXSrouce.Play();
    }
    public void playCoin()
    {
        SFXSrouce.clip = coin;
        SFXSrouce.Play();
    }
    public void playAttack()
    {
        SFXSrouce.clip = attack;
        SFXSrouce.Play();
    }
    public void GetHit()
    {
        SFXSrouce.clip = getHit;
        SFXSrouce.Play();
    }
    public void RestoreHealth()
    {
        SFXSrouce.clip = heal;
        SFXSrouce.Play();
    }

    public void RestoreMana()
    {
        SFXSrouce.clip = mana;
        SFXSrouce.Play();
    }
    public void DestroyObject()
    {
        SFXSrouce.clip = destroyObject;
        SFXSrouce.Play();
    }

    public void Portal()
    {
        SFXSrouce.clip = portal;
        SFXSrouce.Play();
    }

    public void openChest()
    {
        SFXSrouce.clip = chestOpen;
        SFXSrouce.Play();
    }

    public void DialogueNext()
    {
        SFXSrouce.clip = dialogue;
        SFXSrouce.Play();
    }
}
