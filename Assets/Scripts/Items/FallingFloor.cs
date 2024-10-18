using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingFloor : MonoBehaviour
{
    [SerializeField] private float beforeFallCounter = 5f;
    [ReadOnly]
    [SerializeField] private bool willFall;
    private Rigidbody2D rb2D;

    void Start()
    {
        willFall = false;
        rb2D = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        GameObject lemmingGameObject = col.transform.root.gameObject;

        if (lemmingGameObject.layer == LayerMask.NameToLayer("Lemming") || lemmingGameObject.layer == LayerMask.NameToLayer("Skull"))
        {
            StartCoroutine(StartFallCounter());
        }
    }

    public IEnumerator StartFallCounter()
    {
        yield return new WaitForSeconds(beforeFallCounter);

        willFall = true;
        SwitchToDynamic();
    }

    private void SwitchToDynamic()
    {
        if (rb2D != null)
        {
            rb2D.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
