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
    GameObject instantiatedObject = Instantiate(objectPrefab, transform.position, Quaternion.identity);

    instantiatedObject.transform.SetParent(transform.parent);

    instantiatedObject.transform.localPosition = Vector3.zero;

    DragHandler.Instance.StartDragging(instantiatedObject);
}







}
