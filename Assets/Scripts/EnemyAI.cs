using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float chaseRadius;
    public float attackRadius;

    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsTilemap;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;

    public int currentHealth;
    public int maxHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;

        currentHealth = maxHealth;
    }

    private void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, chaseRadius, WhatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, WhatIsPlayer);

        dir = target.position - transform.position;
        dir.Normalize();
        movement = dir;

        if (isInChaseRange)
        {
            anim.SetBool("IsMoving", true);
            anim.SetFloat("X", movement.x);
            anim.SetFloat("Y", movement.y);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
    }

    private void FixedUpdate()
    {
        if (isInChaseRange && !isInAttackRange)
        {
            // Check for obstacles using raycast to detect Tilemap
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, chaseRadius, WhatIsTilemap);

            if (hit.collider == null)
            {
                // No Tilemap obstacle, continue chasing the player
                MoveCharacter(movement);
            }
            else
            {
                // Adjust the movement direction to go around the Tilemap obstacle
                Vector2 avoidanceDir = Vector2.Perpendicular(hit.normal).normalized;
                movement = avoidanceDir;
                MoveCharacter(movement);
            }
        }

        if (isInAttackRange)
        {
            if (!anim.GetBool("IsAttacking")) // Check if not already attacking
            {

                anim.SetBool("IsAttacking", true);
            }
        }
        else
        {
            anim.SetBool("IsMoving", true);
            anim.SetBool("IsAttacking", false);
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0) {

            Destroy(gameObject);
        }
    }


}
