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
                GoldenManagement.instance.IncreaseGolds(50); //nb gold à voir 
                Destroy(gameObject);

            }else if(CompareTag("Lemming")){
                GoldenManagement.instance.IncreaseGolds(100); //nb gold à voir 
                gameObject.SetActive(false);
            }
        }
    }
}
