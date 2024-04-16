using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private string key_id;
    AudioManager audioManager;

    [ContextMenu("Generate Guild Id")]
    private void GenerateGuidId()
    {
         key_id= Guid.NewGuid().ToString();
    }

    [SerializeField]
    private int value;
    private bool hasTriggered;

    private KeyManager keyManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>(); 

        if (string.IsNullOrEmpty(key_id))
            GenerateGuidId();
    }
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
            audioManager.SFX_Play(audioManager.collectibles);
            Destroy(gameObject);
        }
    }
    public void LoadData(GameData data)
    {
        data.keysCollected.TryGetValue(key_id, out hasTriggered);

        if (hasTriggered)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.keysCollected.ContainsKey(key_id))
        {
            data.keysCollected.Remove(key_id);
        }

        data.keysCollected.Add(key_id, hasTriggered);
    }


}
