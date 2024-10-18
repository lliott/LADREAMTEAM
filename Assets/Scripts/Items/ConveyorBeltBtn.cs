using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltBtn : MonoBehaviour
{
    [SerializeField] private GameObject conveyorBelt;

    //Btn
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float buttonPressDistance = -0.3f;
    [SerializeField] private float delayAfterLastLemmingLeaves = 5f;

    [Header("Number of lemmings needed to open door")]
    [SerializeField] private int lemmingsNeeded = 1;
    private Vector3 buttonOriginalPos;
    private Vector3 buttonPressedPos;
    private bool isMoving = false;
    private int numberOfLemmingsOnButton = 0;
    private Coroutine releaseButtonCoroutine;

    private void Start()
    {
        buttonOriginalPos = transform.position;
        buttonPressedPos = buttonOriginalPos + new Vector3(0, buttonPressDistance, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lemming"))
        {
            numberOfLemmingsOnButton++;

            if (numberOfLemmingsOnButton == lemmingsNeeded)
            {
                if (releaseButtonCoroutine != null)
                {
                    StopCoroutine(releaseButtonCoroutine);
                    releaseButtonCoroutine = null;
                }

                StartCoroutine(PressButton());
            }
        }
    }

     private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lemming"))
        {
            numberOfLemmingsOnButton--;

            if (numberOfLemmingsOnButton < 0)
            {
                numberOfLemmingsOnButton = 0;
            }

            if (numberOfLemmingsOnButton < lemmingsNeeded)
            {
                if (releaseButtonCoroutine == null)
                {
                    releaseButtonCoroutine = StartCoroutine(DelayedReleaseButton());
                }
            }
        }

    }

    private IEnumerator PressButton()
    {
        if (!isMoving)
        {
            isMoving = true;

            while (Vector3.Distance(transform.position, buttonPressedPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, buttonPressedPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = buttonPressedPos;
            conveyorBelt.GetComponent<ConveyorBelt>().ChangeDir();
            isMoving = false;
        }
    }

    private IEnumerator DelayedReleaseButton()
    {
        yield return new WaitForSeconds(delayAfterLastLemmingLeaves);

        if (numberOfLemmingsOnButton == 0)
        {
            yield return StartCoroutine(ReleaseButton());

        } else {

            releaseButtonCoroutine = null;
        }
    }

    private IEnumerator ReleaseButton()
    {
        if (!isMoving)
        {
            isMoving = true;

            while (Vector3.Distance(transform.position, buttonOriginalPos) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, buttonOriginalPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = buttonOriginalPos;
            conveyorBelt.GetComponent<ConveyorBelt>().ChangeDir();
            isMoving = false;
        }

        releaseButtonCoroutine = null;
    }


    // private void OnMouseOver()
    // {
    //     if (Input.GetMouseButtonDown(0)){
    //         conveyorBelt.GetComponent<ConveyorBelt>().ChangeDir();
    //     }
    // }
}
