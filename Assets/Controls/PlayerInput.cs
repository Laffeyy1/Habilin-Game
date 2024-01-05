using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;

    [SerializeField]
    private InputActionReference movement, attack, pointerPosition;

    private DialogueManager dialogueManager;
    private QuizManager quizManager;
    public bool isBlocked;

    AudioManager audioManager;
    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if (dialogueManager != null && dialogueManager.isDone)
        {
            OnMovementInput?.Invoke(movement.action.ReadValue<Vector2>().normalized);
            OnPointerInput?.Invoke(GetPointerInput());

            isBlocked = false;
        }
        else
        {
            OnMovementInput?.Invoke(Vector2.zero);

            if (!isBlocked)
            {
                isBlocked = true; // Set the flag to true to prevent spamming
            }
        }
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = pointerPosition.action.ReadValue<Vector2>();
        mousePos.z = Camera.main.nearClipPlane;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

    private void OnEnable()
    {
        attack.action.performed += PerformAttack;
    }

    private void PerformAttack(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }

    private void OnDisable()
    {
        attack.action.performed -= PerformAttack;
    }

    public void OnAttackSFX()
    {
        audioManager.playAttack();
    }
}
