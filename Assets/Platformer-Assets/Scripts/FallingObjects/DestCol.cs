using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestCol : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) 
    {
        if (col.gameObject.CompareTag("FallingObject")) 
        {
            Destroy(col.gameObject);
        }
    }
}
