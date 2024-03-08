using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyyingEnemy : MonoBehaviour
{

    public DetectArea flyingEnemyBite;
    public bool _hasTarget = false;

    public float reachWaypoints= 0.1f;
    public List<Transform> wayPoints;
    public float flySpeed = 2f;

    int numWaypoints =0 ;
    Transform nextWayPoint;

    Animator animator;
    Damageable damageable;
    Rigidbody2D rb;
 
    public bool HasTarget
    {
        get

        { return _hasTarget; }

        private set
        {

            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    public bool CanMove 
    { get { return animator.GetBool(AnimationStrings.canMove); } }

    private void Start()
    {
        nextWayPoint = wayPoints[numWaypoints];
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }

    void Update()
    {
        HasTarget = flyingEnemyBite.detectColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive) 
        {
            if (CanMove) 
            {
                Fly();

            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        
        }
    }

    private void Fly()
    {
        Vector2 directionWaypoints = (nextWayPoint.position - transform.position).normalized;

        float distance = Vector2.Distance(nextWayPoint.position,transform.position);
        rb.velocity = directionWaypoints * flySpeed;

        FlipFlyDirection();

        if(distance <= reachWaypoints) 
        {
            numWaypoints++;

            if(numWaypoints >= wayPoints.Count) 
            {
                numWaypoints = 0;
            }
            nextWayPoint = wayPoints[numWaypoints];
        
        }
    }

    private void FlipFlyDirection()
    {
        Vector3 enemyScale = transform.localScale;
        
        if(transform.localScale.x > 0) 
        {
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * enemyScale.x, enemyScale.y, enemyScale.z);
            }
        }
        else 
        {
            if(rb.velocity.x > 0) 
            {
                transform.localScale = new Vector3(-1 * enemyScale.x, enemyScale.y, enemyScale.z);
            }
        
        }
        
    }
}
