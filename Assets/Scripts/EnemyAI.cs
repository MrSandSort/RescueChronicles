using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;

    public bool shouldRotate;
    public LayerMask WhatIsPlayer;
    public LayerMask WhatIsTilemap; // Add this line to specify the Tilemap layer

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;
    private bool isCollidingWithTilemap; // New variable to track Tilemap collisions

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        anim.SetBool("IsMoving", isInChaseRange);
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, WhatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, WhatIsPlayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;

        if (shouldRotate)
        {
            anim.SetFloat("X", dir.x);
            anim.SetFloat("Y", dir.y);
        }
    }

    private void FixedUpdate()
    {
        // Check for collisions with Tilemap
        isCollidingWithTilemap = Physics2D.Raycast(transform.position, dir, checkRadius, WhatIsTilemap);

        if (isInChaseRange && !isInAttackRange && !isCollidingWithTilemap)
        {
            MoveCharacter(movement);
        }

        if (isInAttackRange)
        {
            anim.SetBool("IsMoving", false);
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }
}
