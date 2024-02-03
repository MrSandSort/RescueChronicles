using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public bool isWalking;
    public Rigidbody2D rb;
    public Animator animator;
    public bool isAttacking;

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
        if (!isAttacking)  // Check if the player is not attacking
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
            StopMoving();  // Stop movement when attacking
        }
    }

    
    void HandleAttackInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isAttacking = true;
            animator.SetBool("IsAttacking", isAttacking);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
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
