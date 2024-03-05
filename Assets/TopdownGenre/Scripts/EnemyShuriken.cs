using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShuriken : MonoBehaviour
{
    private GameObject player;
    public Rigidbody2D rb;
    public float force;

    private float timer;
    public int shurikenDamage;
    private bool playerAlive = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");       
    }

    void Update()
    {
        if (!playerAlive) 
            return;

        Vector2 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rotate = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotate + 90);

        timer += Time.deltaTime;

        if (timer > 10) 
        {
            Destroy(gameObject);
        
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!playerAlive)
            return;

        if (col.gameObject.CompareTag("Player")) 
        {
            HealthManager playerHp = col.gameObject.GetComponent<HealthManager>();

            if (playerHp != null)
            {
                playerHp.currentHealth -= shurikenDamage;
                Destroy(gameObject);

                if (playerHp.currentHealth <= 0)
                {
                    col.gameObject.SetActive(false);
                    playerAlive= false;
                }
            }

        }  
    }
}
