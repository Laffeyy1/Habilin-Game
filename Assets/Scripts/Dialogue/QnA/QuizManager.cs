using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    private TMP_Text nameText;
    private TMP_Text dialogueText;
    private Image speakerImage;
    public GameObject Trigger;

    private GameObject dialogueUI;

    public bool isDone;

    [Header("Button")]
    private GameObject buttonGrid; // Where buttons will be placed
    public GameObject buttonPrefab;

    [Header("Rewards")]
    public GameObject[] RewardPrefab;
    public GameObject[] PunishmentPrefab;

    private QuestionScene currentQuestionScene;
    private Queue<QuestionScene.Sentence> sentences;
    private Coroutine typingCoroutine; // Store the reference to the typing coroutine.

    private bool isLastSentence; // Track if the current sentence is the last one.

    void Start()
    {
        GameObject dialogue = GameObject.FindGameObjectWithTag("Dialogue");
        dialogueUI = dialogue.transform.GetChild(0).gameObject;

        GameObject image = dialogueUI.transform.GetChild(0).gameObject;
        GameObject player = image.transform.GetChild(0).gameObject;

        speakerImage = player.transform.GetChild(0).GetComponent<Image>();
        nameText = dialogueUI.transform.GetChild(1).GetComponent<TMP_Text>();
        dialogueText = dialogueUI.transform.GetChild(2).GetComponent<TMP_Text>();
        buttonGrid = dialogueUI.transform.GetChild(3).gameObject;

        sentences = new Queue<QuestionScene.Sentence>();
        isDone = true;
    }

    public void StartDialogue(QuestionScene questionScene)
    {
        Debug.Log("Starting dialogue...");
        isDone = false;
        sentences.Clear();

        currentQuestionScene = questionScene;
        foreach (QuestionScene.Sentence sentence in questionScene.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (typingCoroutine != null)
        {
            // If a typing coroutine is running, stop it and show the entire sentence.
            StopCoroutine(typingCoroutine);
            dialogueText.text = sentences.Count > 0 ? sentences.Peek().text : string.Empty; // Show the entire sentence.
        }

        if (sentences.Count == 0)
        {
            isLastSentence = true; // Mark that this is the last sentence.
            DisplayButtons(); // Display buttons when there are no more sentences.
            return;
        }

        QuestionScene.Sentence sentence = sentences.Dequeue();

        // Display speaker's name
        speakerImage.sprite = sentence.speaker != null ? sentence.speaker.speakerSprite : null;
        nameText.text = sentence.speaker != null ? sentence.speaker.speakerName : "";

        typingCoroutine = StartCoroutine(TypeSpeed(sentence.text));
    }

    IEnumerator TypeSpeed(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

        typingCoroutine = null; // Clear the coroutine reference.
    }

    void DisplayButtons()
    {
        // Clear existing buttons in the buttonGrid.
        foreach (Transform child in buttonGrid.transform)
        {
            Destroy(child.gameObject);
        }

        // Instantiate the buttonPrefab for the correct answer.
        GameObject correctButton = Instantiate(buttonPrefab, buttonGrid.transform);
        correctButton.GetComponentInChildren<TMP_Text>().text = currentQuestionScene.correctAnswer; // Set button text.
        correctButton.GetComponent<Button>().onClick.AddListener(CorrectButtonClicked);

        // Instantiate the buttonPrefab for the wrong answer.
        GameObject wrongButton = Instantiate(buttonPrefab, buttonGrid.transform);
        wrongButton.GetComponentInChildren<TMP_Text>().text = currentQuestionScene.wrongAnswer; // Set button text.
        wrongButton.GetComponent<Button>().onClick.AddListener(WrongButtonClicked);
    }

    private void CorrectButtonClicked()
    {
        // Implement the behavior when the correct answer button is clicked.
        DropRandomReward();
        DestroyButtons();
    }

    private void WrongButtonClicked()
    {
        // Implement the behavior when the wrong answer button is clicked.
        DropRandomEnemy();
        DestroyButtons();
    }

    private void DropRandomReward()
    {
        if (RewardPrefab != null && RewardPrefab.Length > 0)
        {
            // Choose a random item prefab from the array
            GameObject droppedItemPrefab = RewardPrefab[UnityEngine.Random.Range(0, RewardPrefab.Length)];

            if (droppedItemPrefab != null)
            {
                // Instantiate the chosen item prefab at the Trigger's position
                GameObject droppedItem = Instantiate(droppedItemPrefab, Trigger.transform.position, Quaternion.identity);

                // Apply a force to the dropped item (e.g., if it's an item that should fall)
                Rigidbody2D itemRigidbody = droppedItem.GetComponent<Rigidbody2D>();
                if (itemRigidbody != null)
                {
                    Debug.Log("Correct answer clicked!");
                    // Adjust this force as needed
                    itemRigidbody.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
                }
            }
        }
        EndDialogue();
    }

    private void DropRandomEnemy()
    {
        if (PunishmentPrefab != null && PunishmentPrefab.Length > 0)
        {
            // Choose a random item prefab from the array
            GameObject droppedItemPrefab = PunishmentPrefab[UnityEngine.Random.Range(0, PunishmentPrefab.Length)];

            if (droppedItemPrefab != null)
            {
                // Instantiate the chosen item prefab at the Trigger's position
                GameObject droppedItem = Instantiate(droppedItemPrefab, Trigger.transform.position, Quaternion.identity);

                // Apply a force to the dropped item (e.g., if it's an item that should fall)
                Rigidbody2D itemRigidbody = droppedItem.GetComponent<Rigidbody2D>();
                if (itemRigidbody != null)
                {
                    Debug.Log("Wrong answer clicked!");
                    // Adjust this force as needed
                    itemRigidbody.AddForce(Vector2.down * 5f, ForceMode2D.Impulse);
                }
            }
        }
        EndDialogue();
    }

    private void DestroyButtons()
    {
        foreach (Transform child in buttonGrid.transform)
        {
            Destroy(child.gameObject);
        }
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        dialogueUI.SetActive(false); // Disable the dialogue UI when the queue is empty.
        isDone = true;
        Trigger.SetActive(false);
    }

    void Update()
    {
        if (!isDone && !isLastSentence)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                DisplayNextSentence();
            }
        }
    }
}
