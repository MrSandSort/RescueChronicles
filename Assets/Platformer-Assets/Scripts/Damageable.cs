using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    Animator animator;
    public UnityEvent<int,Vector2> damagableHit;

    [SerializeField]
    public bool isInv = false;

    private float sinceHit = 0;
    public float invTimer = 0.25f;

    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth 
    {
        get 
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private int _health = 100;

    public int Health 
    {
        get 
        {
            return _health;
        }
        set
        {
            _health = value;

            if (_health <= 0) 
            {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    public bool IsAlive 
    {
        get 
        {
            return _isAlive;
        }

        set 
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
           
        }
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (isInv) 
        {

            if (sinceHit>invTimer) 
            {

                isInv = false;
                sinceHit = 0;
            }

            sinceHit += Time.deltaTime;
        }
    }
    public bool Hit(int damage, Vector2 knockback) 
    {
    
        if(IsAlive && !isInv) 
        {

            Health -= damage;
            isInv = true;

            animator.SetTrigger(AnimationStrings.hit);
            damagableHit?.Invoke(damage, knockback);

            return true;
        }
        
        return false;
    }
  
}
