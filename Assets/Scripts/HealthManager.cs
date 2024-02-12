using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage) {

        currentHealth -= damage;

        if (currentHealth <= 0) {

            gameObject.SetActive(false);
        }

    }
}
