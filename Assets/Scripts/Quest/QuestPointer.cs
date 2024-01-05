using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPointer : MonoBehaviour
{
    public string questTag = "Quest";
    public Transform player; // Assuming you have a player character.
    public float pointerSpeed = 5.0f;
    public float padding = 20.0f; // Padding to keep the pointer away from the screen edges.
    public Transform questPointer; // Public variable to assign the pointer in the Unity editor.

    private Transform target;
    private Canvas canvas;
    private RectTransform canvasRect;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();

        if (questPointer != null)
        {
            questPointer.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                return;
            }
        }

        GameObject questObject = GameObject.FindGameObjectWithTag(questTag);

        if (questObject != null)
        {
            if (questPointer != null)
            {
                questPointer.gameObject.SetActive(true);
            }

            target = questObject.transform;

            Vector2 screenPos = Camera.main.WorldToScreenPoint(target.position);
            Vector2 pointerPos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPos, null, out pointerPos);

            // Clamp the pointer to stay within the screen bounds.
            pointerPos.x = Mathf.Clamp(pointerPos.x, -canvasRect.rect.width / 2 + padding, canvasRect.rect.width / 2 - padding);
            pointerPos.y = Mathf.Clamp(pointerPos.y, -canvasRect.rect.height / 2 + padding, canvasRect.rect.height / 2 - padding);

            if (questPointer != null)
            {
                questPointer.localPosition = pointerPos;
            }

            Vector3 direction = (target.position - player.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (questPointer != null)
            {
                questPointer.rotation = Quaternion.Euler(0, 0, angle);
            }

            // Move the pointer towards the quest (optional).
            if (questPointer != null)
            {
                questPointer.position = Vector3.Lerp(questPointer.position, target.position, pointerSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (questPointer != null)
            {
                questPointer.gameObject.SetActive(false);
            }
        }
    }
}
