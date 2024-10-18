using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltBtn : MonoBehaviour
{
    [SerializeField] private GameObject conveyorBelt;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)){
            conveyorBelt.GetComponent<ConveyorBelt>().ChangeDir();
        }
    }
}
