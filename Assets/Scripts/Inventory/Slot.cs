using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler
{
    public GameObject objectPrefab; 
    private GameObject currentDraggedObject;
    private GoldenController _goldController;

  void Start(){
    _goldController = GetComponent<GoldenController>();
  }

  void Update(){ //Opti?
    if(_goldController.CanBuy()){
        //couleur
        GetComponent<Image>().color = Color.white;
    }else{
      //couleur
      GetComponent<Image>().color = Color.red;
    }
  }

//Drag
  public void OnPointerDown(PointerEventData eventData)
  {
    if(_goldController.CanBuy())
    {
      //produce object
      GameObject instantiatedObject = Instantiate(objectPrefab, transform.localPosition, Quaternion.identity);
      instantiatedObject.transform.SetParent(objectPrefab.transform.parent);

      //dim golden
      _goldController.PayObject();

      //Drag
      DragHandler.instance.StartDragging(instantiatedObject);
    } 
    else{
      Debug.Log("peut pas acheter " + objectPrefab.name);
    }
  }


}
