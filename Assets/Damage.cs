using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public MCHealthManager healthMan;

    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        healthMan = FindAnyObjectByType<MCHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            healthMan.health = healthMan.health - damage;
        }
    }*/


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag ("Player"))
        {
           
            if (healthMan != null)
            {
                healthMan.health -= damage; 
            }
        }
       
    }


}
