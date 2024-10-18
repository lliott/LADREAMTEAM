using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private int nbWinner; // nb de lemmings qui doivent passer le portail pr gagner la game
    [SerializeField] private GameObject MenuFin;
    public int currentWinner;
    private bool won=false;

    void Start(){
        MenuFin.SetActive(false);
        ResumeGame();
    }

    void Update(){
        if (currentWinner == nbWinner && !won){
            won = true;
            
            PauseGame();
            MenuFin.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D collider2D){
        if (collider2D.tag=="Lemming"){
            currentWinner+=1;

            Destroy(collider2D.gameObject);
            
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
