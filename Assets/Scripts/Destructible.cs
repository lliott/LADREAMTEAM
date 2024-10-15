using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) // clic droit
        {
            if (CompareTag("Object"))
            {
                Destroy(gameObject);

            }else if(CompareTag("Lemming")){
                gameObject.SetActive(false);
            }
        }
    }
}
