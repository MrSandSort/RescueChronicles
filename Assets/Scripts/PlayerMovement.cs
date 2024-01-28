using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float x, y;
    public float moveSpeed= 5f;
    public bool isWalking;
    public Rigidbody2D rb;
    public Vector3 movedir;
    public Animator animator;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
       
        x= Input.GetAxisRaw("Horizontal");
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

        else {

            if (isWalking) {

                isWalking = false;
                animator.SetBool("IsWalking", isWalking);
                StopMoving();
            }
        }

        movedir = new Vector3(x, y).normalized;

    }

    void StopMoving() {
        rb.velocity = Vector3.zero;
    }

    void FixedUpdate()
    {
        rb.velocity = movedir * moveSpeed * Time.deltaTime;

    }
}
