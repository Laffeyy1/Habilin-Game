using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSelection : MonoBehaviour
{
    public Ability[] skills;
    [SerializeField] GameObject skillUI;

    TMP_Text name;
    TMP_Text desc;
    Image icon;

    int selectedSkill = 0;

    private void Start()
    {
        name = skillUI.transform.Find("name").GetComponent<TMP_Text>();
        desc = skillUI.transform.Find("desc").GetComponent<TMP_Text>();
        icon = skillUI.transform.Find("image").GetComponent<Image>();
        name.text = skills[selectedSkill].name;
        desc.text = skills[selectedSkill].description;
        icon.sprite = skills[selectedSkill].icon;
        PlayerPrefs.SetInt("AbilitySelected", selectedSkill);
    }

    public void NextSkill()
    {
        selectedSkill = (selectedSkill + 1) % skills.Length;
        name.text = skills[selectedSkill].name;
        desc.text = skills[selectedSkill].description;
        icon.sprite = skills[selectedSkill].icon;
        PlayerPrefs.SetInt("AbilitySelected", selectedSkill);
    }

    public void PreviousSkill()
    {
        selectedSkill--;
        if(selectedSkill < 0)
        {
            selectedSkill += skills.Length;
        }
        name.text = skills[selectedSkill].name;
        desc.text = skills[selectedSkill].description;
        icon.sprite = skills[selectedSkill].icon;
        PlayerPrefs.SetInt("AbilitySelected", selectedSkill);
    }
}
