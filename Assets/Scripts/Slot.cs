using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler
{
    public GameObject objectPrefab; 
    private GameObject currentDraggedObject;

  public void OnPointerDown(PointerEventData eventData)
{
    // Instantiate the object at the slot's world position with no rotation
    GameObject instantiatedObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);

    // Set the instantiated object to be a child of the prefab's parent
    instantiatedObject.transform.SetParent(transform.parent);

    // Set the local position to zero so it is centered in the parent
    instantiatedObject.transform.localPosition = Vector3.zero;

    // Start dragging the object
    DragHandler.Instance.StartDragging(instantiatedObject);
}







}
