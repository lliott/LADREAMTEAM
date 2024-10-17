using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private int nbWinner = 1; // nb de lemmings qui doivent passer le portail pr gagner la game
    [SerializeField] private GameObject MenuFin;
    private int currentWinner;
    private bool won=false;

    void Start(){
        MenuFin.SetActive(false);
        ResumeGame();
    }

    void Update(){
        if (currentWinner == nbWinner && !won){
            won = true;
            Debug.Log("A gagné lancer la win");
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.tag=="Lemming"){
            Debug.Log("win");
            currentWinner+=1;

            Destroy(collider2D.gameObject);
            
            PauseGame();
            MenuFin.SetActive(true);
        }
    }

    //Pause
     void PauseGame()   //Met sur Pause
    {
        Time.timeScale = 0;
        MenuFin.SetActive(true);
    }

    public void ResumeGame(){ //Reprendre la game
        MenuFin.SetActive(false);
        Time.timeScale = 1;
    }

}
