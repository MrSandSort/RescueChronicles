using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot: MonoBehaviour
{
    private GameObject plr;
    public Rigidbody2D rb;
    public float force;

    private float tim;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        plr = GameObject.FindGameObjectWithTag("Player");

        Vector2 direction = plr.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rotate = Mathf.Atan2(-direction.x, -direction.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotate+270);
    }

    void Update()
    {
        tim+= Time.deltaTime;

        if (tim > 2) 
        {
            Destroy(gameObject);
        }
    }

}
