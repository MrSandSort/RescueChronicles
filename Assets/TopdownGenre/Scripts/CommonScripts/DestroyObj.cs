using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObj : MonoBehaviour
{
    [SerializeField]
    private float destroyInSeconds = 0.8f;
    void Start()
    {
        Destroy(gameObject, destroyInSeconds);
    }

    
}
