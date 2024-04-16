using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Platformer_PlayerController player;
    private BoxCollider2D checkPointCollider;

    [SerializeField]
    private GameObject SavedPanel;

    private void Start()
    {
        checkPointCollider = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            SavedPanel.SetActive(true);
            DataPersistenceManager.instance.SaveGame();
            checkPointCollider.enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D another)
    {
        if (another.gameObject.tag == "Player")
        {
            SavedPanel.SetActive(false);
        }
    }
}
