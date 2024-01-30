using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float x, y;
    public float moveSpeed = 5f;
    public bool isWalking;
    public Rigidbody2D rb;
    public Vector3 movedir;
    public Animator animator;
    public bool isAttacking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (x != 0 || y != 0)
        {
            animator.SetFloat("X", x);
            animator.SetFloat("Y", y);

            if (!isWalking)
            {
                isWalking = true;
                animator.SetBool("IsWalking", isWalking);
            }
        }
        else
        {
            if (isWalking)
            {
                isWalking = false;
                animator.SetBool("IsWalking", isWalking);
                StopMoving();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleAttack();
        }

        movedir = new Vector3(x, y).normalized;
    }

    void EndAttackAnimation()
    {
        // This method is called by an animation event when the attack animation ends
        isAttacking = false;
        animator.SetBool("IsAttacking", isAttacking);
    }

    void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        if (!isAttacking)
        {
            rb.velocity = movedir * moveSpeed * Time.deltaTime;
        }
        else
        {
            StopMoving();
        }
    }

    void ToggleAttack()
    {
        isAttacking = !isAttacking;
        animator.SetBool("IsAttacking", isAttacking);
    }
}
