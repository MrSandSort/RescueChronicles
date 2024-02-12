using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{

    void Start()
    {
       
    }
    private void OnCollisionEnter2D(Collision2D another)
    {
        if (another.collider.tag == "Player") {

            another.gameObject.GetComponent<HealthManager>().DamagePlayer(10);
        }
    }
}

   /* private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasDealtDamage)
        {
            hasDealtDamage = true; // Mark that damage has been dealt to prevent multiple hits
            EnemyAI enemy = GetComponentInParent<EnemyAI>(); // Assuming the EnemyAI script is attached to the parent

            int damage = enemy.damage;

            if (healthMan != null)
            {
                healthMan.health -= damage;
            }
        }
    }

    // OnTriggerExit2D is called when the collider exits the trigger area
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hasDealtDamage = false; // Reset the flag when the player leaves the trigger area
        }
    }
}
*/