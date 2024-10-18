using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenController : MonoBehaviour
{
    [HideInInspector] public bool canBuy;
    private int price;
    private Text _textPrice;

     void Start(){
        _textPrice = GetComponentInChildren<Text>();
        if(GetComponent<Slot>().objectPrefab!=null){    
            price = GetComponent<Slot>().objectPrefab.GetComponent<Destructible>().objectPrice;
        }

        _textPrice.text = price.ToString();
    }


    public void PayObject(){
        GoldenManagement.instance.MinusGolds(price);
    }

   public bool CanBuy(){
    if(GoldenManagement.instance.currentCoins >= price){
        return true;
    }
    return false;
   }

}
