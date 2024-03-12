using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField]
    private int value;
    private bool hasTriggered;

    private DiamondManager diamondManager;

    private void Start()
    {
        diamondManager = DiamondManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")&& !hasTriggered)
        {
            hasTriggered = true;
            diamondManager.ChangeDiamonds(value);
            Destroy(gameObject);

        }
    }
}
