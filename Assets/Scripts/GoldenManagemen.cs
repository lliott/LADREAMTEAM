using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenManagement : MonoBehaviour
{
    public static GoldenManagement instance;
    [SerializeField] private Text _textCurrentCoins;
    [SerializeField] private int _initCoins = 50;
    private int _currentCoins;

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

    // Start is called before the first frame update
    void Start()
    {
        _currentCoins = _initCoins;
        _UpdateUI();
    }

    public void MinusGolden(int golden){
        _currentCoins -= golden;
        _UpdateUI();
    }

    private void _UpdateUI(){
        _textCurrentCoins.text = "coins :" + _currentCoins;
    }

}
