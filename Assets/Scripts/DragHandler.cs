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

        // init
        RectTransform rectTransform = draggedObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = Input.mousePosition;
    }

    void Update()
    {
        if (isDragging && draggedObject != null)
        {
            //suit souris
            RectTransform rectTransform = draggedObject.GetComponent<RectTransform>();
            rectTransform.position = Input.mousePosition;

            //drop
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
