using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public bool isWalking;
    public Rigidbody2D rb;
    public Animator animator;
    public bool isAttacking;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    private int attackCount = 0; // Track the number of attacks
    private float holdDuration = 0.5f; // Duration for holding space bar before stopping attack animation
    private float spacePressedTime = 0f; // Track the time when space bar is pressed

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        if (DialogManager.isActive == true)
        {
            return;
        }

        HandleMovementInput();
        HandleAttackInput();
    }

    void HandleMovementInput()
    {
        if (!isAttacking) // Check if the player is not attacking
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

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
                }
            }

            Vector3 movedir = new Vector3(x, y).normalized;
            rb.velocity = movedir * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Stop player movement while attacking
            rb.velocity = Vector3.zero;
            isWalking = false;
            animator.SetBool("IsWalking", isWalking);
        }
    }

    void HandleAttackInput()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetKeyDown(KeyCode.Space)) // Check if there are fewer than 2 attacks
            {
                isAttacking = true;
                animator.SetBool("IsAttacking", isAttacking);
                nextAttackTime = Time.time + 1f / attackRate;
                attackCount++; // Increment the attack count
                spacePressedTime = Time.time; // Record the time when space bar is pressed
            }
            else if (attackCount >= 2 && Time.time >= nextAttackTime) // Reset attack count after cooldown period
            {
                attackCount = 0;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            EndAttackAnimation();
        }

        // Check if the space bar has been held down for longer than holdDuration
        if (isAttacking && Input.GetKey(KeyCode.Space) && Time.time >= spacePressedTime + holdDuration)
        {
            EndAttackAnimation();
        }
    }

    void EndAttackAnimation()
    {
        isAttacking = false;
        animator.SetBool("IsAttacking", isAttacking);
    }
}
