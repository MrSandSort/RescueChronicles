using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectArea : MonoBehaviour
{
    public UnityEvent noCollider;
    public List<Collider2D> detectColliders = new List<Collider2D>();

    Collider2D col;
    private void wake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D another)
    {
        detectColliders.Add(another);
    }

    private void OnTriggerExit2D(Collider2D another)
    {
        detectColliders.Remove(another);

        if (detectColliders.Count <= 0) 
        {
            noCollider.Invoke();
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
