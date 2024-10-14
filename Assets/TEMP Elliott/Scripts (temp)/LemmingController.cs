using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingController : MonoBehaviour
{
    [Header("Lemmies")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float killLemmiTimer = 7f;
    private float currentTimerCounter = 0f;
    [SerializeField] private float coyoteTimeDuration = 0.5f;
    private float coyoteTimeCounter = 0f;

    [Header("public values")]
    public bool grounded = false;
    public bool walled = false;
    private bool movingRight = true;

    private int Floor;
    private int Wall;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        Floor = LayerMask.NameToLayer("Floor");
        Wall = LayerMask.NameToLayer("Wall");

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        KillLemmiOhNo();
        MoveLemming();
        FlipLemming();

        if (coyoteTimeCounter > 0f)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == Floor)
        {
            grounded = true;
            coyoteTimeCounter = 0f;
        }

        if (col.gameObject.layer == Wall)
        {
            walled = true;
            ChangeDirection();
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == Floor)
        {
            grounded = false;
            coyoteTimeCounter = coyoteTimeDuration;
        }

        if (col.gameObject.layer == Wall)
        {
            walled = false;
        }
    }

    private void MoveLemming()
    {
        if (grounded || coyoteTimeCounter > 0f)
        {
            if (movingRight)
            {
                this.gameObject.transform.position += new Vector3(speed * Time.deltaTime, 0, 0); // Move right
            }
            else
            {
                this.gameObject.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0); // Move left
            }
        }
    }

    private void ChangeDirection()
    {
        movingRight = !movingRight;
    }

    private void FlipLemming()
    {
        if (movingRight)
        {
            spriteRenderer.flipX = false;

        }
        else
        {

            spriteRenderer.flipX = true;
        }
    }

    private void KillLemmiOhNo()
    {
        if (!grounded)
        {
            currentTimerCounter += Time.deltaTime;
        }
        else
        {
            currentTimerCounter = 0f;
        }

        if (currentTimerCounter > killLemmiTimer)
        {
            Destroy(gameObject);
        }
    }


}
