using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingGround), typeof(Damageable))]
public class Platformer_PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    public float walkSpeed = 5f;
    public float airWalkSpeed = 3f;
    public float runSpeed = 8f;
    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        return airWalkSpeed;
                    }

                }
                else
                {
                    return 0;
                }

            }

            else
            {
                return 0;
            }
        }
    }

    [SerializeField]
    private float jumpImpulseMultiplier = 1.5f;

    TouchingGround touchingDirections;
    Damageable damageable;

    Rigidbody2D rb;

    Animator animator;
    private bool _isFacingRight = true;

    [SerializeField]
    private float normalJumpImpulse = 8f;

    [SerializeField]
    private float fallDamageThreshold = 10f;

    [SerializeField]
    private float fallDamageMultiplier = 2f;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set

        {
            _isMoving = value;

            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set

        {
            _isRunning = value;

            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {

            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);

        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingGround>();
        damageable = GetComponent<Damageable>();
    }

    private void Update()
    {
        if (DialogManager.isActive == true)
        {
            animator.SetBool("lockVelocity", true);
            return;
        }
        else if (DialogManager.isActive == false)
        {
            animator.SetBool("lockVelocity", false);
            return;
        };

    }
    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);

        if (rb.velocity.y < -fallDamageThreshold && !touchingDirections.IsGrounded)
        {
            int fallDamage = Mathf.RoundToInt(Mathf.Abs(rb.velocity.y) * fallDamageMultiplier); 
            damageable.Hit(fallDamage, Vector2.zero);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }

    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {

            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            IsRunning = true;
        }
        else if (context.canceled)
        {

            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.started && touchingDirections.IsGrounded && CanMove)

        {
            animator.SetTrigger(AnimationStrings.jump);

            rb.velocity = new Vector2(rb.velocity.x, normalJumpImpulse);

        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Sprinter"))
        {
            rb.velocity = new Vector2(rb.velocity.x, normalJumpImpulse * jumpImpulseMultiplier);

        }

    }
}
