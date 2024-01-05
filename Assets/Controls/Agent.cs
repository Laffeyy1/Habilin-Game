using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Agent : MonoBehaviour
{
    private CharacterAnimator characterAnimator;
    private CharacterMover characterMover;

    public WeaponParent weaponParent;

    private Transform weaponSlot;

    private Vector2 movementInput;
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    public void PerformAttack()
    {
        weaponParent.Attack();
    }

    private void Awake()
    {
        characterAnimator = GetComponentInChildren<CharacterAnimator>();
        characterMover = GetComponent<CharacterMover>();
        weaponSlot = transform.Find("WeaponSlot");
    }

    private void Update()
    {
        //pointerInput = GetPointerInput();
        //movementInput = movement.action.ReadValue<Vector2>().normalized;

        characterMover.MovementInput = movementInput;
        if (weaponParent != null)
        {
            weaponParent.SetPoiterposition(movementInput);
        }

        AnimateCharacter();
    }

    private void AnimateCharacter()
    {
        // Calculate the direction of movement (normalized)
        Vector2 moveDirection = movementInput.normalized;

        // Rotate the character to face the look direction
        characterAnimator.RotateToPointer(moveDirection);

        // Play animation based on movement input
        characterAnimator.PlayAnimation(moveDirection);
    }
}
