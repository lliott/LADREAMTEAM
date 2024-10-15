using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenController : MonoBehaviour
{
    [HideInInspector] public bool canBuy;
    [SerializeField] private int price;
    private Text _textPrice;

     void Start(){
        _textPrice = GetComponentInChildren<Text>();
        _textPrice.text = price.ToString();
    }


    public void PayObject(){
        GoldenManagement.instance.MinusGolden(price);
    }

   public bool CanBuy(){
    if(GoldenManagement.instance.currentCoins >= price){
        return true;
    }
    return false;
   }

}
