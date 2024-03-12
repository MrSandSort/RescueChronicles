using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingGround), typeof(Damageable))]
public class EnemyPlatform : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float walkStopRate = 0.05f;

    public DetectArea attackArea;
    public DetectArea cliffDetectionArea;

    Animator animator;
    Damageable damageable;
    Rigidbody2D rb;

    public enum WalkableDirection { Right, Left }

    private WalkableDirection _walkDirection;

    TouchingGround touchingDirection;
    public WalkableDirection WalkDirection
    {
        get
        {
            return _walkDirection;
        }
        private set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }

            _walkDirection = value;
        }
    }

    public bool _hasTarget = false;

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

    public bool CanMove { get { return animator.GetBool(AnimationStrings.canMove); } }

    public float AttackCooldown
    {
        get
        {
            return animator.GetFloat(AnimationStrings.attackCooldown);
        }
        private set 
        {

            animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value,0));
        }
    }

    public Vector2 walkDirectionVector = Vector2.left;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingGround>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    private void Update()
    {
        HasTarget = attackArea.detectColliders.Count > 0;

        if (AttackCooldown>0) 
        {
            AttackCooldown -= Time.deltaTime;
        }
       
    }
    private void FixedUpdate()
    {
        if (touchingDirection.IsGrounded && touchingDirection.IsOnWall) 
        {
            FlipDirection();
        }

        if (!damageable.LockVelocity)
        {
            if (CanMove)
            {
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }

        }


    }

    private void FlipDirection()
    {
      if(WalkDirection== WalkableDirection.Right)
        
      {
            WalkDirection = WalkableDirection.Left;
      }
      else if(WalkDirection== WalkableDirection.Left) 
      {
            WalkDirection = WalkableDirection.Right;
      }
      else
      {
            Debug.LogError("Not set"); 
      }
    }

    public void OnHit(int damage, Vector2 knockback)
    { 
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);

    }

    public void OnCliffDetection() 
    {

        if (touchingDirection.IsGrounded) 
        {
            FlipDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D box)
    {
        if (box.gameObject.tag=="Box")
        {
            FlipDirection();
        }


    }

   
}
