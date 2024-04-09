using System;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingGround), typeof(Damageable))]
public class Platformer_PlayerController : MonoBehaviour, IDataPersistence
{
    private float horizontal;
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

    [SerializeField] float wallSlideSpeed = 0f;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] Vector2 wallCheckSize;

    [SerializeField] float wallJumpForce = 18f;
    [SerializeField] float wallJumpDirection = -1;
    [SerializeField] Vector2 wallJumpAngle;

    private bool isTouchingWall;
    private bool isWallSliding;
    private bool canJump;


    [SerializeField]
    private float jumpImpulseMultiplier = 1.5f;

    TouchingGround touchingDirections;
    Damageable damageable;

    Rigidbody2D rb;

    Animator animator;
    private bool _isFacingRight = true;

    private bool IsWalled()
    {
        return isTouchingWall = Physics2D.OverlapBox(wallCheck.position, wallCheckSize, 0, wallLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallCheck.position, wallCheckSize);
    }

    [SerializeField]
    private float normalJumpImpulse = 8f;

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

        wallJumpAngle.Normalize();
    }

    private void Update()
    {
        IsWalled();
        Jump();

        horizontal = Input.GetAxisRaw("Horizontal");

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
        WallSlide();
        WallJump();

        if (!damageable.LockVelocity)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);


        /* if (rb.velocity.y < -fallDamageThreshold && !touchingDirections.IsGrounded)
         {
             int fallDamage = Mathf.RoundToInt(Mathf.Abs(rb.velocity.y) * fallDamageMultiplier); 
             damageable.Hit(fallDamage, Vector2.zero);
         }*/
    }

    public void LoadData(GameData data ) 
    {
        this.transform.position = data.playerPos;
    }
    public void SaveData(ref GameData data) 
    {
        data.playerPos = this.transform.position;
    }


    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            canJump = true;
        }
    }

    private void WallSlide()
    {
        if (isTouchingWall && !touchingDirections.IsGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }


    }

    private void WallJump()
    {
        if ((isWallSliding || isTouchingWall) && canJump)
        {
            rb.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
            canJump = false;
        }
    }

    void Flip()
    {
        wallJumpDirection *= -1;
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0, 180, 0);
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

    public void OnRangeAttack(InputAction.CallbackContext context)
    {

        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangeAttack);
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
        else if (other.gameObject.tag == "FallingObject")
        {
            damageable.Health = 0;
            Destroy(other.gameObject, 2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Spikes")
        {
            damageable.Health = 0;
        }
        else if (other.gameObject.tag == "Spear")
        {
            damageable.Health = 0;
        }
    }

}
