using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectPrefab; 

    //Hover Scale
    private Vector3 _scaleInit;
    [SerializeField] private GameObject _slotSprite;
    
    private GameObject currentDraggedObject;

    void Start(){
      _scaleInit = _slotSprite.transform.localScale;
      _slotSprite.transform.localScale = _scaleInit;
    }

//Drag
  public void OnPointerDown(PointerEventData eventData)
  {
      GameObject instantiatedObject = Instantiate(objectPrefab, transform.localPosition, Quaternion.identity);

      instantiatedObject.transform.SetParent(objectPrefab.transform.parent);

      DragHandler.Instance.StartDragging(instantiatedObject);
  }

//Hover
    public void OnPointerEnter(PointerEventData eventData)
    {
       _slotSprite.transform.localScale = _scaleInit *1.1f;
       Debug.Log("yaaaa");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _slotSprite.transform.localScale = _scaleInit;
    }







}
