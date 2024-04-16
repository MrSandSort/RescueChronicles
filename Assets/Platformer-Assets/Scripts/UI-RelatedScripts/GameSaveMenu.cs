using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveMenu : MonoBehaviour
{
    public GameObject GameSavePanel;
    void Start()
    {
        Time.timeScale = 1f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            Debug.Log("Checkpoint Found!");
        }
    }


}
