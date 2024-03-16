using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Checkpoints : MonoBehaviour
{
    private Respawn_Player respawn;

    private void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Respawn_Player>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            respawn.respawnPoint = this.gameObject;
        }
    }
}
