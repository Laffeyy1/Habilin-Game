using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSelection : MonoBehaviour,IDataPersistence
{
    [Header("Buttons For Lapu Lapu Chapters")]
    public GameObject lapuChapterHolder;
    public Button lapuChp1;
    public Button lapuChp2;
    public Button lapuChp3;
    public Button lapuChp4;

    [Header("Buttons For Andres Chapters")]
    public GameObject andresChapterHolder;
    public Button andresChp1;
    public Button andresChp2;
    public Button andresChp3;
    public Button andresChp4;
    public Button andresChp5;

    public Button endless;

    private int selectedCharacter;

    private bool lapuChapter1Finished;
    private bool lapuChapter2Finished;
    private bool lapuChapter3Finished;
    private bool lapuChapter4Finished;

    private bool andresChapter1Finished;
    private bool andresChapter2Finished;
    private bool andresChapter3Finished;
    private bool andresChapter4Finished;
    private bool andresChapter5Finished;

    void Start()
    {
       selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
    }

    public void OnClick()
    {
        if(selectedCharacter == 0)
        {
            lapuChapterHolder.SetActive(true);

            // Determine the number of chapters completed
            int completedChapters = 1
                ;
            if (lapuChapter1Finished) completedChapters++;
            if (lapuChapter2Finished) completedChapters++;
            if (lapuChapter3Finished) completedChapters++;
            if (lapuChapter4Finished) completedChapters++;

            // Enable buttons based on completed chapters
            lapuChp1.interactable = completedChapters >= 1;
            lapuChp2.interactable = completedChapters >= 2;
            lapuChp3.interactable = completedChapters >= 3;
            lapuChp4.interactable = completedChapters >= 4;

            endless.interactable = completedChapters >= 5;
        }
        if(selectedCharacter == 1)
        {
            andresChapterHolder.SetActive(true);

            int completedChapters = 1;

            if (andresChapter1Finished) completedChapters++;
            if (andresChapter2Finished) completedChapters++;
            if (andresChapter3Finished) completedChapters++;
            if (andresChapter4Finished) completedChapters++;
            if (andresChapter5Finished) completedChapters++;

            andresChp1.interactable = completedChapters >= 1;
            andresChp2.interactable = completedChapters >= 2;
            andresChp3.interactable = completedChapters >= 3;
            andresChp4.interactable = completedChapters >= 4;
            andresChp5.interactable = completedChapters >= 5;

            endless.interactable = completedChapters >= 6;
        }
        else
        {
            Debug.Log("No Character selected");
        }
    }

    public void LoadData(GameData data)
    {
        lapuChapter1Finished = data.lapuChapter1Finished;
        lapuChapter2Finished = data.lapuChapter2Finished;
        lapuChapter3Finished = data.lapuChapter3Finished;
        lapuChapter4Finished = data.lapuChapter4Finished;

        andresChapter1Finished = data.andresChapter1Finished;
        andresChapter2Finished = data.andresChapter2Finished;
        andresChapter3Finished = data.andresChapter3Finished;
        andresChapter4Finished = data.andresChapter4Finished;
        andresChapter5Finished = data.andresChapter5Finished;
    }

    public void SaveData(GameData data)
    {
        // No need to save, only load
    }
}
