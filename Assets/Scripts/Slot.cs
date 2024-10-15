using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler
{
    public GameObject objectPrefab; 
    private GameObject currentDraggedObject;


//Drag
  public void OnPointerDown(PointerEventData eventData)
  {
      //produce object
      GameObject instantiatedObject = Instantiate(objectPrefab, transform.localPosition, Quaternion.identity);
      instantiatedObject.transform.SetParent(objectPrefab.transform.parent);

      //dim golden
      GetComponent<GoldenController>().PayObject();

      //Drag
      DragHandler.instance.StartDragging(instantiatedObject);
  }


}
