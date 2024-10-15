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

    [Header("Ground Detection")]
    [SerializeField] private float groundCheckDistance = 0.3f;  // Increase the distance to ensure ground detection
    [SerializeField] private LayerMask groundLayerMask;         // LayerMask for "Floor" and "Button" layers
    [SerializeField] private Transform groundCheckPosition;     // Add a position to cast the ray from (feet)
    public bool grounded = false;
    private bool wasGrounded = false;

    [Header("Public Values")]
    public bool walled = false;
    private bool movingRight = true;

    private int Wall;
    private SpriteRenderer spriteRenderer;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        Wall = LayerMask.NameToLayer("Wall");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        movingRight = true;
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
        CheckIfGrounded();   // Perform the raycast to check if grounded
        MoveLemming();
        FlipLemming();
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckDistance, groundLayerMask);

        Debug.DrawRay(groundCheckPosition.position, Vector2.down * groundCheckDistance, Color.red);

        if (hit.collider != null)
        {
            grounded = true;
            coyoteTimeCounter = 0f;
        }
        else
        {
            grounded = false;
            if (wasGrounded)
            {
                coyoteTimeCounter = coyoteTimeDuration;
            }
        }
        wasGrounded = grounded;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == Wall)
        {
            walled = true;
            Debug.Log("Lemming hit a wall: " + col.gameObject.name);  // Debug wall collision
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.layer == Wall)
        {
            walled = false;
        }
    }

    private void MoveLemming()
    {
        if (walled)
        {
            ChangeDirection();
            walled = false;
        }

        if (grounded || (!grounded && coyoteTimeCounter > 0f))
        {
            // Movement direction is based on whether the Lemming is moving right or left
            moveDirection.Set(movingRight ? speed * Time.fixedDeltaTime : -speed * Time.fixedDeltaTime, 0, 0);

            // Move the Lemming in the calculated direction
            transform.position += moveDirection;
        } else
        {
            return;
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
