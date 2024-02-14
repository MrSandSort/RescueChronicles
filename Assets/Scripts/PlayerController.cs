using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    public float speed;

    private Animator animator;
    private float attackTime = 0.25f;
    private float attackCounter = 0.25f;
    private bool isAttacking;

   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (DialogManager.isActive == true)
        {
            return;
        }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Set velocity based on input
        rb.velocity = new Vector2(horizontalInput, verticalInput) * speed * Time.deltaTime;

        // Set animator parameters for movement
        animator.SetFloat("X", rb.velocity.x);
        animator.SetFloat("Y", rb.velocity.y);

        // Set animator parameters for last movement direction
        if (horizontalInput == 1 || horizontalInput == -1 || verticalInput == -1 || verticalInput == 1)
        {
            animator.SetFloat("lastX", horizontalInput);
            animator.SetFloat("lastY", verticalInput);
        }

        if (isAttacking)
        {
            attackCounter -= Time.deltaTime;

            if (attackCounter <= 0)
            {
                // Reset attack animation
                animator.SetBool("IsAttacking", false);
                isAttacking = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isAttacking)
        {
            attackCounter = attackTime;
            // Trigger attack animation
            animator.SetBool("IsAttacking", true);
            isAttacking = true;
        }

    }


    /*    void HandleAttackInput()
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
        }*/


}





/* public float moveSpeed;
 public bool isWalking;
 public Rigidbody2D rb;
 public Animator animator;

 private Vector2 MoveDir;


 private float attackTime = 0.25f;

 private float attackCoolDown = 0.25f;

 private bool IsAttacking;

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

     if (IsAttacking)
     {

         attackCoolDown -= Time.deltaTime;
         if (attackCoolDown <=0 )
         {
             animator.SetBool("IsAttacking", false);
             IsAttacking = false;
         }
     }

     if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
     {
         attackCoolDown = attackTime;
         AttackAnimate();
     }
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

 void AttackAnimate()
 {

     animator.SetBool("IsAttacking", true);
     IsAttacking = true;

 }
*/


