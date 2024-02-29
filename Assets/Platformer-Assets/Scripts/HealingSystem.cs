using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSystem : MonoBehaviour
{
    public int healHealth = 20;

    public Vector3 spinRotation = new Vector3(0,180,0);

   
    void Start()
    {
        
    }

    private void Update()
    {
        transform.eulerAngles += spinRotation * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D healTrigger)
    {
        Damageable damageable = healTrigger.GetComponent<Damageable>();
        
        if (damageable) 
        {
           bool isHealed= damageable.Heal(healHealth);

           if (isHealed) 
            {
               Destroy(gameObject);
            }

          
        }
    }
}
