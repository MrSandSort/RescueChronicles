using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScripts : MonoBehaviour
{
    Collider2D attackCollider;
    Rigidbody2D rb;

    public int attackDamage= 10;

    public Vector2 knockback = Vector2.zero;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable!= null) 
        {
           bool gotHit= damageable.Hit(attackDamage, knockback);

            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + attackDamage);

            }
           
        }
    }
}
