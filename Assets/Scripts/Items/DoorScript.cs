using System.Collections;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject door;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float buttonPressDistance = -0.3f;
    [SerializeField] private float delayAfterLastLemmingLeaves = 0.5f;

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
            OpenDoor();
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
            CloseDoor();
            isMoving = false;
        }

        releaseButtonCoroutine = null;
    }

    private void OpenDoor()
    {
        //door.SetActive(false);
        door.GetComponent<Collider2D>().enabled = false;
       
        door.GetComponent<Animator>().SetBool("Open",true);
    }

    private void CloseDoor()
    {
        //door.SetActive(true);
        door.GetComponent<Collider2D>().enabled = true;
        door.GetComponent<Animator>().SetBool("Open",false);

    }
}
