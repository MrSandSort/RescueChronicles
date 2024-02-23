using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScripts : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage= 20;

    public Vector2 knockBack = Vector2.zero;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable!= null) 
        {
           bool gotHit= damageable.Hit(attackDamage, knockBack);

           if (gotHit) 
           {
                Debug.Log(collision.name + "hit for" + attackDamage);
            }


           
        }
    }
}
