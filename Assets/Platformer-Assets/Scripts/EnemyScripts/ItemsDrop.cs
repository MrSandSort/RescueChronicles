using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsDrop : MonoBehaviour
{
    private Rigidbody2D rb;
    public float dropForce= 5;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up* dropForce, ForceMode2D.Impulse);
    }

    void Update()
    {
        
    }
}
