using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingSystem : MonoBehaviour
{
    public int healHealth = 20;
    AudioManager audioManager;

    public Vector3 spinRotation = new Vector3(0,180,0);

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Update()
    {
        transform.eulerAngles += spinRotation * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D healTrigger)
    {
         Damageable damageable = healTrigger.GetComponent<Damageable>();

         if (damageable) 
         {
            bool isHealed= damageable.Heal(healHealth);

            if (isHealed)
                
             {
                audioManager.SFX_Play(audioManager.Heal);
                gameObject.SetActive(false);
             }

         }
    }

   /* public void LoadData(GameData data)
    {
        data.healingConsumed.TryGetValue(heal_id, out isConsumed);

        if (isConsumed) 
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.healingConsumed.ContainsKey(heal_id))
        {
            data.healingConsumed.Remove(heal_id);
        }

        data.healingConsumed.Add(heal_id, isConsumed);
    }*/
}
