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
    [SerializeField] private float groundCheckDistance = 0.3f;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private Transform groundCheckPosition;
    public bool grounded = false;
    private bool wasGrounded = false;

    [Header("Wall Detection")]
    [SerializeField] private float wallCheckDistance = 0.3f;
    [SerializeField] private LayerMask wallLayerMask;
    [SerializeField] private Transform wallCheckPosition;
    public bool walled = false;
    private int wallSide;

    private Collider2D lemmingCollider;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lemmingCollider = GetComponent<Collider2D>();
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
        CheckIfGrounded();
        CheckIfWalled();
        MoveLemming();
        FlipLemming();
        ResolveCollisions();
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckDistance, groundLayerMask);

        Debug.DrawRay(groundCheckPosition.position, Vector2.down * groundCheckDistance, Color.red);

        if (hit.collider != null)
        {
            grounded = true;
            coyoteTimeCounter = 0f;

        } else {

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

        } else {

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


    private void ChangeDirection()
    {
        movingRight = !movingRight;
    }

    private void FlipLemming()
    {
        transform.rotation = movingRight ? Quaternion.identity : Quaternion.Euler(0f, 180f, 0f);
    }

    private void KillLemmiCondition()
    {
        if (!grounded)
        {
            currentTimerCounter += Time.deltaTime;

        } else {

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
