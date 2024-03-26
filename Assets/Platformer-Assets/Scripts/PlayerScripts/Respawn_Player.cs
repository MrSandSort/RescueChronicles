using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn_Player : MonoBehaviour
{
    public GameObject player;

    public GameObject respawnPoint;

    Damageable damageable;
    
    private int savedHealth;
    
    void Start()
    {
        damageable = player.GetComponent<Damageable>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SaveHealthBeforeRespawn();
            player.transform.position = respawnPoint.transform.position;
            RestoreHealthAfterRespawn();
        }
    }

    public void SaveHealthBeforeRespawn()
    {
        savedHealth = damageable.Health; 
    }

    public void RestoreHealthAfterRespawn()
    {
        damageable.Health = savedHealth;
    }
}
