using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour
{
    [ReadOnly]
    [SerializeField] private int whoTouchedMe = 0;
    [SerializeField] private int theyTouchedMe = 2;
    private Rigidbody2D rb2D;

    private HashSet<GameObject> lemmingsTouched = new HashSet<GameObject>();
    void Start()
    {
        theyTouchedMe += 1;
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Lemming"))
        {
            if (!lemmingsTouched.Contains(col.gameObject))
            {
                whoTouchedMe += 1;
                lemmingsTouched.Add(col.gameObject);

                Debug.Log("Lemming entered: " + col.gameObject.name + ". Total: " + whoTouchedMe);

                if (whoTouchedMe >= theyTouchedMe)
                {
                    SwitchToDynamic();
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Lemming"))
        {
            if (lemmingsTouched.Contains(col.gameObject))
            {
                lemmingsTouched.Remove(col.gameObject);

                Debug.Log("Lemming exited: " + col.gameObject.name);
            }
        }
    }

    public void SwitchToDynamic()
    {
        if (rb2D != null)
        {
            rb2D.bodyType = RigidbodyType2D.Dynamic;
            Debug.Log("Floor is now dynamic!");
        }
    }
}
