using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LemmingController : MonoBehaviour
{
    [Header("Lemmies")]
    public float speed = 5f;
    [SerializeField] private float killLemmiFallTimer = 7f;
    private bool canKillLemmi = false;
    [ReadOnly]
    [SerializeField] private float currentTimerCounter = 0f;
    [SerializeField] private float coyoteTimeDuration = 0.5f;
    private float coyoteTimeCounter = 0f;

    [Header("Stop All Movement")]
    public bool stopped = false;

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

    [HideInInspector] public Vector3 moveDirection = Vector3.zero; // appelé ds ConveyorBelt
    private Collider2D lemmingCollider;
    private bool movingRight = true;
    private SpriteRenderer spriteRenderer;
    private Animator animator;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        lemmingCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on " + gameObject.name);
        }
    }

    private void OnEnable()
    {
        stopped = false;
        currentTimerCounter = 0;
        grounded = false;
        canKillLemmi = false;
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
        KillLemmiFromFall();
    }

    private void CheckIfGrounded()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckDistance, groundLayerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(groundCheckPosition2.position, Vector2.down, groundCheckDistance, groundLayerMask);

        Debug.DrawRay(groundCheckPosition.position, Vector2.down * groundCheckDistance, Color.red);
        Debug.DrawRay(groundCheckPosition2.position, Vector2.down * groundCheckDistance, Color.red);

        if (hit1.collider != null || hit2.collider != null)
        {
            animator.SetBool("isFalling", false);
            grounded = true;
            coyoteTimeCounter = 0f;
        }
        else
        {
            animator.SetBool("isFalling", true);
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
                    break; // Exit inner loop
                }
            }

            if (walled)
                break; // Exit outer loop
        }
    }

    private void MoveLemming()
    {
        if (!stopped)
        {
            if (walled)
            {
                ChangeDirection();
                walled = false;
            }

            if (grounded || (!grounded && coyoteTimeCounter > 0f))
            {
                moveDirection.Set(movingRight ? speed * Time.fixedDeltaTime : -speed * Time.fixedDeltaTime, 0, 0);

                animator.SetBool("isWalking", true);
                transform.position += moveDirection;

            }
            else
            {

                animator.SetBool("isWalking", false);
                return;
            }

        } else { return; }

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

    //Appelé ds ConveyorBelt
    public void ChangeDirection()
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

        if (currentTimerCounter >= killLemmiFallTimer)
        {
            canKillLemmi = true;
        }
    }

    private void KillLemmiFromFall()
    {
        if (grounded && canKillLemmi)
        {
            KillLemmi();
        }
    }

    public void KillLemmi()
    {
        animator.SetTrigger("isDying");
    }
}
