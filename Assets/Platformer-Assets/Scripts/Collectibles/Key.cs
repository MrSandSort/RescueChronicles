using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField]
    private int value;
    private bool hasTriggered;

    private KeyManager keyManager;

    private void Start()
    {
        keyManager = KeyManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true;
            keyManager.AddKeys(value);
            Destroy(gameObject);
        }
    }
}
