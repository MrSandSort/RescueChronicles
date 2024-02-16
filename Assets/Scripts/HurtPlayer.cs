using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int damageTaken;
    public bool isAttacking;
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D another)
    {
        if (another.CompareTag("Player"))
        {
           HealthManager PlayerHealth;
            PlayerHealth = another.gameObject.GetComponent<HealthManager>();
            if (PlayerHealth!= null) {

                PlayerHealth.damagePlayer(damageTaken);
            }
        }
    }

   
}
