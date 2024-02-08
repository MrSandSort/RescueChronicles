using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{

    public bool isAttacking;
    public Animator animator;
    public Rigidbody2D rb;

    public float attackRate = 2f;
    private float nextAttackTime = 0f;
    private int attackCount = 0; // Track the number of attacks
    private float holdDuration = 0.5f; // Duration for holding space bar before stopping attack animation
    private float spacePressedTime = 0f; // Track the time when space bar is pressed

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        HandleAttackInput();
        
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

