using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAI : MonoBehaviour
{
    private Animator anim;
    private Transform target;
    public Transform homePos;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float maxRange;

    [SerializeField]
    private float minRange;

    private void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.FindFirstObjectByType<PlayerMovement>().transform;
    }

    void Update()
    {

        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget >= minRange && distanceToTarget <= maxRange)
        {
            FollowPlayer();
        }
        else if (distanceToTarget < minRange)
        {
            attack();
        }
        else if (distanceToTarget > maxRange)
        {
            getHome();
        }


    }

    private void FollowPlayer()
    {
        anim.SetBool("IsAttacking", false);
        anim.SetBool("withInRange", true);
        anim.SetFloat("X", target.position.x - transform.position.x);
        anim.SetFloat("Y", target.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    private void getHome()
    {
        anim.SetFloat("X", homePos.position.x - transform.position.x);
        anim.SetFloat("Y", homePos.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, homePos.position) == 0)
        {
            anim.SetBool("withInRange", false);
        }
    }

    private void attack()
    {
        anim.SetBool("IsAttacking", true);
        anim.SetFloat("X", target.position.x - transform.position.x);
        anim.SetFloat("Y", target.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

}

    /*public float speed;
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

    public int damage;

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
        MCHealthManager healthMan = GetComponent<MCHealthManager>();
        healthMan.health -= damage;

        if (healthMan.health <= 0) {

            Destroy(gameObject);
        }
    }


}*/

