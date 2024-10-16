using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    [SerializeField] private Transform climbableArea; // Reference to the empty GameObject
    private float scaleX;

    void Start()
    {
        scaleX = transform.localScale.x;
    }

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            scaleX = -scaleX;
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
        
    }

    private bool CanClimb(Collision2D collision)
    {
        // Position escalier : /
        if (transform.localScale.x >= 0)
        {
            return collision.transform.position.x < climbableArea.position.x; // à gauche = true
        }
        // Position escalier : \
        else
        {
            return collision.transform.position.x > climbableArea.position.x; // à droite = true
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lemming"))
        {
            if (CanClimb(collision))
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), false); // detect collision
            }
            else
            {
                Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>(), true); // ignore collision
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Lemming"))
        {
           Physics2D.IgnoreCollision(collision.GetComponent<Collider2D>(), GetComponent<Collider2D>(), false);
        }
    }
}