using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    [SerializeField] private int nbWinner = 1; // nb de lemmings qui doivent passer le portail pr gagner la game
    private int currentWinner;
    private bool won=false;

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
        }
    }

}
