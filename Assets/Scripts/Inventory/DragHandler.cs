using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    public static DragHandler instance;

    private GameObject draggedObject;
    private bool isDragging = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
    }

    void Update()
    {
        if (isDragging && draggedObject != null)
        {
            //suit souris
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0; 
            draggedObject.GetComponent<Transform>().position = mouseWorldPosition;

            //Desactive le collider
            if (draggedObject.TryGetComponent<BoxCollider2D>(out BoxCollider2D coll))
            {
                coll.enabled = false;
            }

            //drop
            if (Input.GetMouseButtonUp(0))
            {
                StopDragging();

                //Active le collider
                if (coll!=null)
                {
                    coll.enabled = true;
                }
            }
        }
    }

    void StopDragging()
    {
        isDragging = false;
        draggedObject = null;
    }
}
