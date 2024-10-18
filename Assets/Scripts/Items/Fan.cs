using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Associer un trigger independant à chaque lemming ds le ventilo
public struct LemmingFlying
{
    public GameObject lemming;
    public bool inTrigger;

    public LemmingFlying(GameObject lemming, bool inTrigger)
    {
        this.lemming = lemming;
        this.inTrigger = inTrigger;
    }
}

public class Fan : MonoBehaviour
{
    private List<LemmingFlying> lemmingsFlying = new List<LemmingFlying>();

    void Update()
    {
        List<LemmingFlying> toRemove = new List<LemmingFlying>();

        foreach (var body in lemmingsFlying)
        {
            var lemming = body.lemming;
            if (lemming.GetComponent<LemmingController>().grounded && !body.inTrigger)  // grounded et pas inTrigger
            {
                toRemove.Add(body);
                lemming.GetComponent<LemmingController>().flying = false;
            }
        }

        foreach (var entry in toRemove)
        {
            lemmingsFlying.Remove(entry);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Lemming")
        {
            // Check si lemming deja ds la liste
            bool alreadyAdded = false;

            for (int i = 0; i < lemmingsFlying.Count; i++)
            {
                if (lemmingsFlying[i].lemming == other.gameObject)
                {
                    alreadyAdded = true;
                    break; // Stop si other ds liste
                }
            }

            if (!other.GetComponent<LemmingController>().flying && !alreadyAdded)
            {
                // si pas add, add + set trigger true
                lemmingsFlying.Add(new LemmingFlying(other.gameObject, true));
                other.GetComponent<LemmingController>().flying = true;
            }
        }
    }

    //mettre à jour le trigger de chq lemming independamment
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Lemming")
        {
            for (int i = 0; i < lemmingsFlying.Count; i++)
            {
                if (lemmingsFlying[i].lemming == other.gameObject)
                {
                    LemmingFlying updatedBody = lemmingsFlying[i];
                    updatedBody.inTrigger = false;
                    lemmingsFlying[i] = updatedBody;
                    break;
                }
            }
        }
    }

}
