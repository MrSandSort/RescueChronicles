using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int,Vector2> damagableHit;
    public UnityEvent<int, int> healthChange;

    Animator animator;

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

            healthChange?.Invoke(Health,MaxHealth);

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

    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);

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
            LockVelocity = true;
            damagableHit?.Invoke(damage, knockback);

            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        
        return false;
    }

    public bool Heal(int healHealth) 
    {
        if (IsAlive && Health < MaxHealth ) 
        {
            int maxHeal = Mathf.Max(MaxHealth- Health,0);
            int actualHeal = Mathf.Min(maxHeal,healHealth);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);

            return true;
        }
        return false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacle") 
        {
            Health = 0;
        }
    }
}
