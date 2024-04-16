using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScreen : MonoBehaviour
{
    Platformer_PlayerController player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Platformer_PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag=="Player") 
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
            player.transform.position = Vector3.zero;

        }
    }
}
