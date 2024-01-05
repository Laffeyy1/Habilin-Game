using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectSaveChapter
{
    None,
    LapuChapter1,
    LapuChapter2,
    LapuChapter3,
    LapuChapter4,
    AndresChapter1,
    AndresChapter2,
    AndresChapter3,
    AndresChapter4,
    AndresChapter5
}
public class SaveChapter : MonoBehaviour,IDataPersistence
{
    [SerializeField]
    private SelectSaveChapter selectedChapter; // Serialized enum to select a chapter

    private bool lapu1;
    private bool lapu2;
    private bool lapu3;
    private bool lapu4;

    private bool andres1;
    private bool andres2;
    private bool andres3;
    private bool andres4;
    private bool andres5;
    private void Start()
    {
        DataPersistenceManager.instance.SaveGame();
    }
    public void LoadData(GameData data)
    {
        lapu1 = data.lapuChapter1Finished;
        lapu2 = data.lapuChapter2Finished;
        lapu3 = data.lapuChapter3Finished;
        lapu4 = data.lapuChapter4Finished;

        andres1 = data.andresChapter1Finished;
        andres2 = data.andresChapter2Finished;
        andres3 = data.andresChapter3Finished;
        andres4 = data.andresChapter4Finished;
        andres5 = data.andresChapter5Finished;
    }

    public void SaveData(GameData data)
    {
        switch (selectedChapter)
        {
            case SelectSaveChapter.LapuChapter1:
                data.lapuChapter1Finished = true;
                break;
            case SelectSaveChapter.LapuChapter2:
                data.lapuChapter2Finished = true;
                break;
            case SelectSaveChapter.LapuChapter3:
                data.lapuChapter3Finished = true;
                break;
            case SelectSaveChapter.LapuChapter4:
                data.lapuChapter4Finished = true;
                break;
            case SelectSaveChapter.AndresChapter1:
                data.andresChapter1Finished = true;
                break;
            case SelectSaveChapter.AndresChapter2:
                data.andresChapter2Finished = true;
                break;
            case SelectSaveChapter.AndresChapter3:
                data.andresChapter3Finished = true;
                break;
            case SelectSaveChapter.AndresChapter4:
                data.andresChapter4Finished = true;
                break;
            case SelectSaveChapter.AndresChapter5:
                data.andresChapter5Finished = true;
                break;
        }
    }
}
