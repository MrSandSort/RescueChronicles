using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingGround))]
public class Platformer_PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    public float walkSpeed= 5f;
    public float airWalkSpeed = 3f;
    public float runSpeed = 8f;

    public float CurrentMoveSpeed
    {
        get
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
    }
       

    TouchingGround touchingDirections;
    Rigidbody2D rb;

    Animator animator;
    private bool _isFacingRight = true;
    public float jumpImpulse= 10f;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set

        { _isMoving = value;

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
        get {
            return _isFacingRight;
        }
        private set {

            if (_isFacingRight != value) 
            {
                transform.localScale *= new Vector2(-1,1);
            }
            _isFacingRight = value;
        } 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingGround>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed , rb.velocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context ) 
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);

    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight) {

            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started) {

            IsRunning = true;
        }
        else if (context.canceled) {

            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context) {

        if (context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
}
