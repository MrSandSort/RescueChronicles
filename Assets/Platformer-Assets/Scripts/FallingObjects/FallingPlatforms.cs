using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    private float fallDelay = 1f;
    private float destroyDelay = 2f;

    [SerializeField]
    private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            StartCoroutine(Fall());
        }
    }
    private IEnumerator Fall() 
    {
        yield return new WaitForSeconds(fallDelay);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);

    }
  
}
