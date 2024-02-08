using UnityEngine;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public bool isWalking;
    public Rigidbody2D rb;
    public Animator animator;
    public bool isAttacking;

    private Vector2 MoveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void StopMoving()
    {
        rb.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (DialogManager.isActive == true)
        {
            return;
        }

        HandleMovementInput();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void HandleMovementInput()
    {
        float x = UnityEngine.Input.GetAxisRaw("Horizontal");
        float y = UnityEngine.Input.GetAxisRaw("Vertical");

        MoveDir = new Vector2(x, y).normalized;
    }

    void Move()
    {
        rb.velocity = new Vector2(MoveDir.x * moveSpeed, MoveDir.y * moveSpeed);




    }


    void Animate()
    {
        animator.SetFloat("X", MoveDir.x);
        animator.SetFloat("Y", MoveDir.y);
        animator.SetFloat("MoveMagnitude", MoveDir.magnitude);
    }
}
