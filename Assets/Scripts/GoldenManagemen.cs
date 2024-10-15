using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenManagement : MonoBehaviour
{
    public static GoldenManagement instance;
    
    [SerializeField] private Text _textCurrentCoins;

    [SerializeField] private int _initCoins = 50;
    public int currentCoins;

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

    void Start()
    {
        currentCoins = _initCoins;
        _UpdateUI();
    }

    void Update(){
        
    }

    public void MinusGolden(int golden){
        currentCoins -= golden;
        _UpdateUI();
    }

    private void _UpdateUI(){
        if(currentCoins<=0){
            _textCurrentCoins.text = "coins : 0" ;
        } else{
            _textCurrentCoins.text = "coins :" + currentCoins;
        }
    }

}
