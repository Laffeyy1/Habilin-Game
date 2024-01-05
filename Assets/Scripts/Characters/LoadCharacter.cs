using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadCharacter : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public GameObject follower;

    public GameObject skillAndres;
    public GameObject skillLapu;

    public Ability[] andresSkills;
    public Ability[] lapuSkills;

    public Transform spawnPoint;
    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        int selectedSkill = PlayerPrefs.GetInt("AbilitySelected", 0);

        GameObject prefab = characterPrefabs[selectedCharacter];
        GameObject clone = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
        if(selectedCharacter == 0)
        {
            skillLapu.GetComponent<Image>().sprite = lapuSkills[selectedSkill].icon;
            skillLapu.SetActive(true);
        }
        else if (selectedCharacter == 1)
        {
            skillAndres.GetComponent<Image>().sprite = andresSkills[selectedSkill].icon;
            skillAndres.SetActive(true);
        }
        else
        {
            Debug.Log("No selected Character");
        }

        follower.SetActive(true);
    }
}
