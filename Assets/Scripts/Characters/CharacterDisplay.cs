using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterDisplay : MonoBehaviour, IDataPersistence
{
    public Character characterInfo;
    public SFXPlayer sfxPlayer;

    public GameObject characterStatsPanel;

    public Ability[] lapuSkills;
    public Ability[] andresSkills;
    public SkillSelection skillSelectionUI;

    private string characterNameGet;
    private string descriptionGet;
    private int IDGet;

    private Sprite spriteGet;
    private Sprite displayImageGet;

    private int healthGet;
    private int attackGet;
    private int manaGet;

    public TMP_Text nameText;
    public TMP_Text descriptionText;

    public TMP_Text characterIDText;

    public Image spriteImage;
    public Image displayImage;

    public Slider healthSlider;
    public Slider attackSlider;
    public Slider manaSlider;

    public Image spriteSkill;
    public TMP_Text skillName;
    public TMP_Text skillDescriptionText;

    [Header("Buying!")]
    public GameObject buyButton;
    public GameObject play;
    public int characterPrice;
    public TMP_Text buyButtonText;
    public GameObject errorDisp;

    public TMP_Text levelText;
    public TMP_Text levelPriceText;
    public GameObject lvlButtonlapu;
    public GameObject skillButtonlapu;
    public GameObject skillButtonandres;
    public TMP_Text skillText;
    public TMP_Text skillPriceText;
    public GameObject errorDispLvl;
    public GameObject errorDispSkill;

    private bool hasBought = false;
    private int lapuLvlup;
    private int andresLvl;
    private int lapuSkillLvl;
    private int andresSkillLvl;

    private void Start()
    {
        IDGet = characterInfo.ID;
        characterNameGet = characterInfo.characterName;
        descriptionGet = characterInfo.description;

        spriteGet = characterInfo.sprite;
        displayImageGet = characterInfo.displayIcon;

        healthGet = characterInfo.health;
        attackGet = characterInfo.speed;
        manaGet = characterInfo.mana;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            CheckMouseClick();
        }
    }

    public void LoadData(GameData data)
    {
        hasBought = data.isAndresUnlocked;

        lapuLvlup = data.lapuLvl;
        andresLvl = data.andresLvl;
        lapuSkillLvl = data.andresSkillLvl;
        andresSkillLvl = data.andresSkillLvl;

        PlayerPrefs.SetInt("PlayerLevel", lapuLvlup);

        Debug.Log("Loaded lapuLvlup: " + lapuLvlup);
    }

    public void SaveData(GameData data)
    {
        data.isAndresUnlocked = hasBought;

        data.lapuLvl = lapuLvlup++;
        data.andresLvl = andresLvl++;
        data.lapuSkillLvl = lapuSkillLvl++;
        data.andresSkillLvl = andresSkillLvl++;
        

        Debug.Log("Leveled up lapu " + data.lapuLvl + data.isAndresUnlocked);
    }

    //check if mouse clicked
    private void CheckMouseClick()
    {
        if (IsMouseOverGameObject())
        {
            sfxPlayer.ButtonAccept();
            CharacterStatsPanel();
            characterStatsPanel.SetActive(true);
        }
    }

    //chheck if the character was selected
    private bool IsMouseOverGameObject()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            return true;
        }
        return false;
    }
    public void onCharacterBuy(int price)
    {
        if (CoinUI.instance.TryDeductCoins(price))
        {
            play.SetActive(true);
            buyButton.SetActive(false);

            hasBought = true;

            DataPersistenceManager.instance.SaveGame();
        }
        else
        {
            errorDisp.SetActive(true);
        }
    }
    public void onLevelBuy()
    {
        string tempStr = skillText.text;
        int price = 50 * lapuLvlup;
        if (CoinUI.instance.TryDeductCoins(price))
        {
            if (IDGet == 0)
            {
                lapuLvlup++;
                levelText.text = "Lvl " + lapuLvlup.ToString();
            }
            else if (IDGet == 1)
            {

                andresLvl++;
                levelText.text = "Lvl " + lapuLvlup.ToString();
            }
            DataPersistenceManager.instance.SaveGame();
        }
        else
        {
            errorDispLvl.GetComponent<TMP_Text>().text = "Not enough coins";
        }
    }

    public void onSkillBuy()
    {
        string tempStr = skillText.text;
        int price = 50 * lapuLvlup;
        DataPersistenceManager.instance.LoadGame();
        if (CoinUI.instance.TryDeductCoins(price))
        {
            if (IDGet == 0)
            {
                lapuSkillLvl++;
                skillText.text = "Lvl " + lapuSkillLvl.ToString();
            }
            else if (IDGet == 1)
            {

                andresSkillLvl++;
                skillText.text = "Lvl " + andresSkillLvl.ToString();
            }
            DataPersistenceManager.instance.SaveGame();
        }
        else
        {
            errorDispSkill.GetComponent<TMP_Text>().text = "Not enough coins";
        }
    }

    private void CharacterStatsPanel()
    {
        characterIDText.text = IDGet.ToString();
        nameText.text = characterNameGet;
        descriptionText.text = descriptionGet;

        spriteImage.sprite = spriteGet;
        displayImage.sprite = displayImageGet;

        healthSlider.value = healthGet;
        attackSlider.value = attackGet;
        manaSlider.value = manaGet;

        if (IDGet == 0)
        {
            skillSelectionUI.skills = lapuSkills;
            play.SetActive(true);
            buyButton.SetActive(false);
            int tempLvl = 50 * lapuLvlup;
            int tempSkill = 50 * lapuSkillLvl;
            levelPriceText.text = tempLvl.ToString();
            skillPriceText.text = tempSkill.ToString();
            levelText.text = "Lvl: " + lapuLvlup.ToString();
            skillText.text = "Lvl: " + lapuSkillLvl.ToString();
            if(tempLvl > CoinUI.instance.coinCount || tempSkill > CoinUI.instance.coinCount)
            {
                lvlButtonlapu.SetActive(false);
                skillButtonlapu.SetActive(false);
            }
            else
            {
                lvlButtonlapu.SetActive(true);
                skillButtonlapu.SetActive(true);
            }
            errorDispLvl.SetActive(false);
            errorDispSkill.SetActive(false);
        }
        else if(IDGet == 1 && hasBought == false)
        {
            skillSelectionUI.skills = andresSkills;
            buyButtonText.text = "<margin-left=1em>" + characterPrice.ToString();
            buyButton.SetActive(true);
            play.SetActive(false);
            int tempLvl = 50 * andresLvl;
            int tempSkill = 50 * andresSkillLvl;
            levelPriceText.text = tempLvl.ToString();
            skillPriceText.text = tempSkill.ToString();
            levelText.text = "Lvl: " + andresLvl.ToString();
            skillText.text = "Lvl: " + andresSkillLvl.ToString();
            errorDispLvl.SetActive(false);
            errorDispSkill.SetActive(false);
            if (tempLvl < CoinUI.instance.coinCount || tempSkill < CoinUI.instance.coinCount)
            {
                lvlButtonlapu.SetActive(false);
                skillButtonlapu.SetActive(false);
            }
            else
            {
                lvlButtonlapu.SetActive(true);
                skillButtonlapu.SetActive(true);
            }
        }
        else if (IDGet == 1 && hasBought == true)
        {
            skillSelectionUI.skills = andresSkills;
            buyButton.SetActive(false);
            play.SetActive(true);
            levelText.text = "Lvl: " + andresLvl;
            skillText.text = "Lvl: " + andresSkillLvl;
            errorDispLvl.SetActive(false);
            errorDispSkill.SetActive(false);
        }
    }
}
