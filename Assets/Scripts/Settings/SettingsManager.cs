using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public TMP_Dropdown difficultyDropdown;

    public Toggle englishToggle;
    public Toggle tagalogToggle;
    

    private void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("Difficulty"));

        if (PlayerPrefs.HasKey("Language"))
        {
            string language = PlayerPrefs.GetString("Language");
            if (language == "English")
            {
                englishToggle.isOn = true;
                tagalogToggle.isOn = false;
            }
            else if (language == "Tagalog")
            {
                englishToggle.isOn = false;
                tagalogToggle.isOn = true;
            }
        }
        else
        {
            PlayerPrefs.SetString("Language", "English");
            PlayerPrefs.Save();
            englishToggle.isOn = true;
            tagalogToggle.isOn = false;
        }

        if (PlayerPrefs.HasKey("Difficulty"))
        {
            int diff = PlayerPrefs.GetInt("Difficulty");
            difficultyDropdown.value = diff - 1;
        }
        else
        {
            PlayerPrefs.SetInt("Difficulty", 2);
            PlayerPrefs.Save();
            difficultyDropdown.value = 1;
        }
    }
    public void SetDifficulty()
    {
        int selectedOption = difficultyDropdown.value;
        PlayerPrefs.SetInt("Difficulty", selectedOption + 1); // Adding 1 to match your PlayerPrefs values
        PlayerPrefs.Save();
    }

    public void SetLanguageToEnglish()
    {
        PlayerPrefs.SetString("Language", "English");
        PlayerPrefs.Save();
    }

    public void SetLanguageToTagalog()
    {
        PlayerPrefs.SetString("Language", "Tagalog");
        PlayerPrefs.Save();
    }
}
