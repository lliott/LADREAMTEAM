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

    [Header("Public Values")]
    public bool grounded = false;
    public bool walled = false;
    private bool movingRight = true;

    private int Floor;
    private int Wall;
    private SpriteRenderer spriteRenderer;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        Floor = LayerMask.NameToLayer("Floor");
        Wall = LayerMask.NameToLayer("Wall");

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        KillLemmiCondition();

        if (coyoteTimeCounter > 0f)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        MoveLemming();
        FlipLemming();
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
        }
    }

    private void OnCollisionExit2D(Collision2D col)
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
            // movement direction is based on : moving right or left
            moveDirection.Set(movingRight ? speed * Time.fixedDeltaTime : -speed * Time.fixedDeltaTime, 0, 0);

            transform.position += moveDirection;
        }
        if (walled)
        {
            ChangeDirection();
            walled = false;
        }
    }

    private void ChangeDirection()
    {
        movingRight = !movingRight;
    }

    private void FlipLemming()
    {
        spriteRenderer.flipX = !movingRight;
    }

    private void KillLemmiCondition()
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
            KillLemmi();
        }
    }
    public void KillLemmi()
    {
        gameObject.SetActive(false);
    }


}
