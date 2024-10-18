using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldenManagement : MonoBehaviour
{
    public static GoldenManagement instance;
    
    [HideInInspector] public int currentCoins;
    
    [SerializeField] private Text _textCurrentCoins;

    [SerializeField] private int _initCoins = 50;
    
    private AudioSource[] _audio = new AudioSource[4];

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

        _audio = GetComponents<AudioSource>();
    }

    public void MinusGolds(int golds){
        currentCoins -= golds;
        _UpdateUI();

        _audio[0].Play();
    }

    public void IncreaseGolds(int golds){
        currentCoins += golds;
        _UpdateUI();

        _audio[1].Play();
    }

    private void _UpdateUI(){
        if(currentCoins<=0){
            _textCurrentCoins.text = "0" ;
        } else{
            _textCurrentCoins.text = currentCoins.ToString();
        }
    }

}
