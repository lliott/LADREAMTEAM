using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LemmiDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }
}
