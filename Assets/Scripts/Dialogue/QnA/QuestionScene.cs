using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStoryScene", menuName = "Data/New Quiz Scene")]
[System.Serializable]
public class QuestionScene : ScriptableObject
{
    public List<Sentence> sentences;

    [System.Serializable]
    public struct Sentence
    {
        public string text;
        public Speaker speaker;
    }
    [Header("Choices")]
    public string correctAnswer;
    public string wrongAnswer;

    [Header("Punishment")]
    public int enemyCount;
}
