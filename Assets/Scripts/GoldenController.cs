using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenController : MonoBehaviour
{
    [SerializeField] private int price;
    [SerializeField] private Text _textPrice;

     void Start(){
        _textPrice.text = price.ToString();
    }


    public void PayObject(){
        GoldenManagement.instance.MinusGolden(price);
    }

   

}
