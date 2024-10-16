using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour
{
    public static DragHandler instance;
    [SerializeField] private RectTransform zone; // zone où le drop possible

    private GameObject draggedObject;
    private bool isDragging = false;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        if(zone==null){
            zone = transform.parent.GetComponent<RectTransform>();
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
            //Destroy en drag
            if(Input.GetMouseButtonUp(1)){
                GoldenManagement.instance.IncreaseGolds(50); //nb gold à voir 
                Destroy(draggedObject);
            }
            
            //suit souris par rapport au world
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = 0; 
            draggedObject.GetComponent<Transform>().position = mouseWorldPosition;

            //Desactive le collider
            if (draggedObject.TryGetComponent<BoxCollider2D>(out BoxCollider2D coll))
            {
                coll.enabled = false;
            }

            //converti position world en screen
            Vector2 mouseScreenPosition = Input.mousePosition;

            //drop dans la zone
            if (Input.GetMouseButtonUp(0)
               && (RectTransformUtility.RectangleContainsScreenPoint(zone, mouseScreenPosition)))
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
