using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonColorController : MonoBehaviour, IDataPersistence
{
   public Button[] buttonsLapu;
    public Button[] buttonsAndres; 

    public Image[] childImagesLapu; 
    public Image[] childImagesAndres;

    public Button endlessMode;

    public Color disabledColor;
    public Color enabledColor;

    private int selectedCharacter;

    private bool[] lapuChapters = new bool[4];
    private bool[] andresChapters = new bool[5];


    void Start()
    {
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");

        if (selectedCharacter == 0)
        {
            int completedChapters = 0;
            UpdateButtonInteractability(buttonsLapu, lapuChapters);
            if (lapuChapters[0]) completedChapters++;
            if (lapuChapters[1]) completedChapters++;
            if (lapuChapters[2]) completedChapters++;
            if (lapuChapters[3]) completedChapters++;

            if (completedChapters >= 4)
            {
                endlessMode.interactable = true;
                endlessMode.transform.Find("Image").GetComponent<Image>().color = enabledColor;
                endlessMode.transform.Find("Image").transform.Find("Border").GetComponent<Image>().color = enabledColor;
            }
            else
            {
                endlessMode.interactable = false;
                endlessMode.transform.Find("Image").GetComponent<Image>().color = disabledColor;
                endlessMode.transform.Find("Image").transform.Find("Border").GetComponent<Image>().color = disabledColor;
            }
        }
        else if (selectedCharacter == 1)
        {
            int completedChapters = 0;
            UpdateButtonInteractability(buttonsAndres, andresChapters);
            if (andresChapters[0]) completedChapters++;
            if (andresChapters[1]) completedChapters++;
            if (andresChapters[2]) completedChapters++;
            if (andresChapters[3]) completedChapters++;
            if (andresChapters[4]) completedChapters++;


            if (completedChapters >= 5)
            {
                endlessMode.interactable = true;
                endlessMode.transform.Find("Image").GetComponent<Image>().color = enabledColor;
                endlessMode.transform.Find("Image").transform.Find("Border").GetComponent<Image>().color = enabledColor;
            }
            else
            {
                endlessMode.interactable = false;
                endlessMode.transform.Find("Image").GetComponent<Image>().color = disabledColor;
                endlessMode.transform.Find("Image").transform.Find("Border").GetComponent<Image>().color = disabledColor;
            }
        }
        else
        {
            Debug.Log("No Character selected");
        }

        // Initialize colors
        SetChildImageColors();
    }

    void UpdateButtonInteractability(Button[] buttons, bool[] chapters)
    {
        for (int i = 0; i < chapters.Length; i++)
        {
            if (chapters[i])
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
                buttons[i].transform.Find("Image").GetComponent<Image>().color = disabledColor;
                buttons[i].transform.Find("Image").transform.Find("Border").GetComponent<Image>().color = disabledColor;
            }

            if (i > 0 && chapters[i - 1])
            {
                buttons[i].interactable = true;
                buttons[i].transform.Find("Image").GetComponent<Image>().color = enabledColor;
                buttons[i].transform.Find("Image").transform.Find("Border").GetComponent<Image>().color = buttons[i].transform.Find("Image").GetComponent<Image>().color = enabledColor;
                ;
            }
        }
    }

    void SetChildImageColors()
    {
        if (selectedCharacter == 0)
        {
            for (int i = 0; i < childImagesLapu.Length; i++)
            {
                childImagesLapu[i].color = buttonsLapu[i].interactable ? enabledColor : disabledColor;
            }
        }
        else if (selectedCharacter == 1)
        {
            for (int i = 0; i < childImagesAndres.Length; i++)
            {
                childImagesAndres[i].color = buttonsAndres[i].interactable ? enabledColor : disabledColor;
            }
        }
    }

    public void LoadData(GameData data)
    {
        // Load chapter completion data
        lapuChapters[0] = data.lapuChapter1Finished;
        lapuChapters[1] = data.lapuChapter2Finished;
        lapuChapters[2] = data.lapuChapter3Finished;
        lapuChapters[3] = data.lapuChapter4Finished;

        andresChapters[0] = data.andresChapter1Finished;
        andresChapters[1] = data.andresChapter2Finished;
        andresChapters[2] = data.andresChapter3Finished;
        andresChapters[3] = data.andresChapter4Finished;
        andresChapters[4] = data.andresChapter5Finished;
    }

    public void SaveData(GameData data)
    {
        
    }
}
