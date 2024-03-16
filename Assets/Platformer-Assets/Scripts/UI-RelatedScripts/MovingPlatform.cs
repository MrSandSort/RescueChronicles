using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private Transform posA, posB;

    [SerializeField]
    private float Speed=0.5f;

    Vector2 targetPos;

    void Start()
    {
        targetPos = posB.position;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, posA.position) < .1f) targetPos= posB.position;
        if (Vector2.Distance(transform.position, posB.position) < .1f) targetPos = posA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed* Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            other.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            other.transform.SetParent(null);
        }
    }
}
