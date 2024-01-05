using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillEn : MonoBehaviour
{
    public GameObject lapuSKill;
    public GameObject andresSkill;
    int selectedCharacter;
    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");

        if(selectedCharacter == 0)
        {
            andresSkill.SetActive(false);
        }
        else if (selectedCharacter == 1)
        {
            lapuSKill.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
