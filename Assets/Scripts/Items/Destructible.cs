using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private float goldPercentIncrease = 50;
    public int objectPrice = 50 ;
    
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1)) // clic droit
        {
            if (CompareTag("Object"))
            {
                GoldenManagement.instance.IncreaseGolds(objectPrice*(int)goldPercentIncrease/100); //nb gold à voir 
                Destroy(gameObject);

            }else if(CompareTag("Lemming")){
                gameObject.SetActive(false); //crane qui roule
                //ELLIOTt C ICI QUI FAUT ADD MERCE
            }
        }
    }
}
