using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Diamond : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private string diamond_Id;

    [SerializeField]
    private int value;
    private bool hasTriggered= false;

    private DiamondManager diamondManager;

    [ContextMenu("Generate guid for Id")]
    private void GenerateGuidId() 
    {
        diamond_Id = Guid.NewGuid().ToString();
    }

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
            gameObject.SetActive(false);

        }
    }

    public void LoadData(GameData data)
    {
        data.diamondsCollected.TryGetValue(diamond_Id, out hasTriggered);
        
        if(hasTriggered)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.diamondsCollected.ContainsKey(diamond_Id)) 
        {
            data.diamondsCollected.Remove(diamond_Id);
        }

        data.diamondsCollected.Add(diamond_Id, hasTriggered);
    }
}
