using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum typeOfPoint
{
    None,
    Minion,
    Boss,
    NextLevel
}

public class EndlesScore : MonoBehaviour
{
    public typeOfPoint typeOfPoint;

    public int minionPoints = 5;
    public int bossPoints = 10;
    public int nextLevelPoints = 15;

    int difficultyMultiplier;
    int totalScore = 0;

    PointTally pointTally;

    private void Start()
    {
        pointTally = GameObject.FindGameObjectWithTag("Point").GetComponent<PointTally>();

        if (PlayerPrefs.HasKey("Difficulty"))
        {
            difficultyMultiplier = PlayerPrefs.GetInt("Difficulty");
        }

        minionPoints *= difficultyMultiplier;
        bossPoints *= difficultyMultiplier;
        nextLevelPoints *= difficultyMultiplier;
    }

    public void OnUpdateScore()
    {
        switch(typeOfPoint)
        {
            case typeOfPoint.Minion:
                pointTally.totalPoints += minionPoints;
                break;
            case typeOfPoint.Boss:
                bossPoints += bossPoints;
                break;
            case typeOfPoint.NextLevel:
                nextLevelPoints += nextLevelPoints;
                break;
            case typeOfPoint.None:
                break;
        }
    }
}
