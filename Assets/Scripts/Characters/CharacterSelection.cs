using EasyTransition;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject characterStatsPanel;
    public TMP_Text characterIDText;

    public string sceneName;
    public DemoLoadScene demoLoadScene;

    public void StartGame()
    {
        // Use the selectedCharacterID for starting the game or any other action you need.
        string characterIDString = characterIDText.text; // Get the text value from the TMPro object

        int characterID;
        if (int.TryParse(characterIDString, out characterID))
        {
            // Successfully parsed the character ID as an integer
            Debug.Log("Starting the game with Character ID: " + characterID);
            // Now you can use the 'characterID' variable as an integer
        }
        else
        {
            // Failed to parse the character ID as an integer
            Debug.LogError("Invalid Character ID: " + characterIDString);
        }

        PlayerPrefs.SetInt("selectedCharacter", characterID);
        demoLoadScene.LoadScene(sceneName);
    }

    public void Back()
    {
        characterStatsPanel.SetActive(false);
    }
}
