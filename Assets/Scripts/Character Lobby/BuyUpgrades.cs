using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyUpgrades : MonoBehaviour, IDataPersistence
{
    public TMP_Text lvlCounter;
    public TMP_Text lvlSkillCounter;

    int lapuLvl;
    int andresLvl;
    int lapuSkillLvl;
    int andresSkillLvl;
    int coins;
    public void LoadData(GameData data)
    {
        coins = data.coins;
        lapuLvl = data.lapuLvl;
        andresLvl = data.andresLvl;
        lapuSkillLvl = data.andresSkillLvl;
        andresSkillLvl = data.andresSkillLvl;
    }

    public void SaveData(GameData data)
    {
        data.coins = coins;
        data.lapuLvl = lapuLvl;
        data.andresLvl = andresLvl;
        data.lapuSkillLvl = lapuSkillLvl;
        data.andresSkillLvl = andresSkillLvl;
    }

    public void BuyLevel()
    {
        int tempPrice = lapuLvl * 100;
        if(coins >= tempPrice)
        {
            lapuLvl++;
            DataPersistenceManager.instance.SaveGame();
        }
    }

    public void BuySkill()
    {

    }
}
