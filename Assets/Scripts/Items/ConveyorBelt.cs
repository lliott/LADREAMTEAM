using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private GameObject lemmingPrefab;
    private float InitSpeed;
    private int DirX = 1;
    private bool sameDirX = true;

    private List<GameObject> lemmingsList = new List<GameObject>(); //lemmings sur le tapis

    void Start(){
        InitSpeed = lemmingPrefab.GetComponent<LemmingController>().speed;
    }

    // Btn 
    public void ChangeDir(){
        DirX *= -1;
        speed *= -1;
        Debug.Log("Direction changed");

        if (lemmingsList.Count > 0)
        {
            foreach (var lemming in lemmingsList)
            {
                lemming.GetComponent<LemmingController>().ChangeDirection();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lemming"))
        {
            sameDirX = (Mathf.Sign(other.GetComponent<LemmingController>().moveDirection.x) == Mathf.Sign(DirX));

            if (!sameDirX)
            {
                other.GetComponent<LemmingController>().ChangeDirection();
            }else{ 
                lemmingsList.Add(other.gameObject);
                other.GetComponent<LemmingController>().speed = speed;
            }

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Lemming"))
        {
            lemmingsList.Remove(other.gameObject);
            other.GetComponent<LemmingController>().speed = InitSpeed;
        }
    }
}

