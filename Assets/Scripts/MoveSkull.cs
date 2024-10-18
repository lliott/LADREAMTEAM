using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSkull : MonoBehaviour
{
    [Header("Lemmies")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float killSkullFallTimer = 7f;
    [SerializeField] private float killSkullTimer = 7f;
    private bool canKillSkull = false;
    [ReadOnly]
    [SerializeField] private float currentTimerCounter = 0f;
    [SerializeField] private float coyoteTimeDuration = 0.5f;
    private float coyoteTimeCounter = 0f;

    [Header("Ground Detection")]
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundCheckPosition;
    [SerializeField] private Transform groundCheckPosition2;
    public bool grounded = false;
    private bool wasGrounded = false;

    [Header("Wall Detection")]
    [SerializeField] private float wallCheckDistance = 0.3f;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private Transform wallCheckPosition;
    [SerializeField] private Transform wallCheckPosition2;
    public bool walled = false;
    private int wallSide;

    [Header("Rotation Settings")]
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private Transform skullSprite;

    private Collider2D lemmingCollider;
    private bool movingRight = true;
    private Vector3 moveDirection = Vector3.zero;
    private int targetLayer;

    void Start()
    {
        if (skullSprite == null)
        {
            Debug.LogError("SkullSprite reference is not assigned in the Inspector.");
        }

        lemmingCollider = GetComponent<Collider2D>();

        currentTimerCounter = 0;
        grounded = false;
        canKillSkull = false;
        movingRight = true;
    }

    private void Update()
    {
        KillSkullCondition();

        if (coyoteTimeCounter > 0f)
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (killSkullTimer > 0f)
        {
            killSkullTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        CheckIfGrounded();
        CheckIfWalled();
        MoveLemming();
        FlipLemming();
        ResolveCollisions();
        KillSkullFromFall();
        KillSkullAfterTimer();
        RotateSkull();
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckDistance, groundLayerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(groundCheckPosition2.position, Vector2.down, groundCheckDistance, groundLayerMask);

        Debug.DrawRay(groundCheckPosition.position, Vector2.down * groundCheckDistance, Color.red);
        Debug.DrawRay(groundCheckPosition2.position, Vector2.down * groundCheckDistance, Color.red);

        if (hit1.collider != null || hit2.collider != null)
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

    private void CheckIfWalled()
    {
        Vector2[] directions = { Vector2.left, Vector2.right };
        walled = false;

        Transform[] wallCheckPositions = { wallCheckPosition, wallCheckPosition2 };

        foreach (Transform wallCheckPosition in wallCheckPositions)
        {
            foreach (Vector2 direction in directions)
            {
                RaycastHit2D hit = Physics2D.Raycast(wallCheckPosition.position, direction, wallCheckDistance, wallLayerMask);

                Debug.DrawRay(wallCheckPosition.position, direction * wallCheckDistance, Color.blue);

                if (hit.collider != null)
                {
                    walled = true;
                    wallSide = direction == Vector2.left ? -1 : 1;
                    break;
                }
            }

            if (walled)
                break;
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
            moveDirection.Set(movingRight ? speed * Time.fixedDeltaTime : -speed * Time.fixedDeltaTime, 0, 0);

            transform.position += moveDirection;
        }
        else
        {
            return;
        }
    }

    private void ResolveCollisions()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.layerMask = wallLayerMask;
        contactFilter.useLayerMask = true;

        List<Collider2D> overlappingColliders = new List<Collider2D>();
        int overlapCount = lemmingCollider.OverlapCollider(contactFilter, overlappingColliders);

        foreach (Collider2D wallCollider in overlappingColliders)
        {
            ColliderDistance2D colliderDistance = lemmingCollider.Distance(wallCollider);

            if (colliderDistance.isOverlapped)
            {
                Vector2 separationVector = colliderDistance.normal * colliderDistance.distance;
                transform.position += (Vector3)separationVector;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targetLayer = LayerMask.NameToLayer("KillZone");

        if (collision.gameObject.layer == targetLayer)
        {
            Destroy(gameObject);
        }
    }

    private void ChangeDirection()
    {
        movingRight = !movingRight;
    }

    private void FlipLemming()
    {
        transform.rotation = movingRight ? Quaternion.identity : Quaternion.Euler(0f, 180f, 0f);
    }

    private void KillSkullCondition()
    {
        if (!grounded)
        {
            currentTimerCounter += Time.deltaTime;
        }
        else
        {
            currentTimerCounter = 0f;
        }

        if (currentTimerCounter >= killSkullFallTimer)
        {
            canKillSkull = true;
        }
    }

    private void KillSkullFromFall()
    {
        if (grounded && canKillSkull)
        {
            KillSkull();
        }
    }

    private void KillSkullAfterTimer()
    {
        if (killSkullTimer <= 0f)
        {
            KillSkull();
        }
    }

    public void KillSkull()
    {
        Destroy(gameObject);
    }


    private void RotateSkull()
    {
        if (skullSprite == null)
            return;

        // clockwise (1) for right, counter-clockwise (-1) for left
        float rotationDirection = movingRight ? -1f : 1f;

        float rotationAmount = rotationSpeed * Time.fixedDeltaTime * rotationDirection;

        skullSprite.Rotate(0f, 0f, rotationAmount);
    }
}
