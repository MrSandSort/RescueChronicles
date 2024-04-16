using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScripts : MonoBehaviour
{
    Damageable damageable;

    AudioManager audioManager;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnCollisionEnter(Collision waterSprites)
    {
        if (waterSprites.gameObject.tag=="Water") 
        {
            damageable.Health = 0;
            audioManager.SFX_Play(audioManager.death);
        }
    }
}
