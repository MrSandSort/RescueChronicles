using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damageTaken;

    void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D another)
    {
        if (another.tag== "Enemy")
        {
            EnemyDealDamage eHealthMan;
            eHealthMan = another.gameObject.GetComponent<EnemyDealDamage>();
            eHealthMan.DamageEnemy(damageTaken);
        }
    }
  
}


