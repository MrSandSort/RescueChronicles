using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectArea : MonoBehaviour
{
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
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
