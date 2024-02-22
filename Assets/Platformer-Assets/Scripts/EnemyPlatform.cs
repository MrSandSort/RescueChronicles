using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingGround))]
public class EnemyPlatform : MonoBehaviour
{
    public float walkSpeed= 3f;
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
                gameObject.transform.localScale = new Vector2 (gameObject.transform.localScale.x* -1, gameObject.transform.localScale.y);

                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value== WalkableDirection.Left) 
                {
                    walkDirectionVector = Vector2.left;
                }
            }

                _walkDirection = value; 
        }
    }

    public Vector2 walkDirectionVector = Vector2.left;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingGround>();
    }

    private void FixedUpdate()
    {
        if (touchingDirection.IsGrounded && touchingDirection.IsOnWall) 
        {
            FlipDirection();
        }
        rb.velocity = new Vector2(walkSpeed* walkDirectionVector.x, rb.velocity.y);

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

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
