using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    public static DragHandler Instance;

    private GameObject draggedObject;
    private bool isDragging = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartDragging(GameObject objectToDrag)
    {
        draggedObject = objectToDrag;
        isDragging = true;

        // Set the position to the mouse position when starting the drag
        RectTransform rectTransform = draggedObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Input.mousePosition;
    }

    void Update()
    {
        if (isDragging && draggedObject != null)
        {
            // Follow the mouse position directly
            RectTransform rectTransform = draggedObject.GetComponent<RectTransform>();
            rectTransform.position = Input.mousePosition;

            // Stop dragging
            if (Input.GetMouseButtonUp(0))
            {
                StopDragging();
            }
        }
    }

    public void StopDragging()
    {
        isDragging = false;
        draggedObject = null;
    }
}
