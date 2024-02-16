using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Platformer_PlayerController : MonoBehaviour
{
    Vector2 moveInput;
    public float walkSpeed= 5f;
    Rigidbody2D rb;

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set

        { _isMoving = value;

            animator.SetBool("isMoving", value);
        }
    }

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

            animator.SetBool("isRunning", value);
        }
    }
    Animator animator;
    private bool _isFacingRight= true;

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
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * walkSpeed , moveInput.y);
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
}
