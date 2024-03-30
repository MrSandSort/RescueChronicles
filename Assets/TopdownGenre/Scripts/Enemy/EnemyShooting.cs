using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject shuriken;
    public Transform shurikenPos;
    private float timer;

    private GameObject player;
    public int distanceOfShooting;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < distanceOfShooting) 
        {
            timer += Time.deltaTime;

            if (timer > 2)
            {
                timer = 0;
                fireballAttack();
            }
        }
     
    }

    void fireballAttack() 
    {

        Instantiate(shuriken, shurikenPos.position, Quaternion.identity);
    }
}
