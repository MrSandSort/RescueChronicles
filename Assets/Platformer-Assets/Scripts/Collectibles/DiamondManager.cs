using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DiamondManager : MonoBehaviour, IDataPersistence
{
    public static DiamondManager instance;
    public int diamonds;

    [SerializeField] private TMP_Text diamondDisplay;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void OnGUI()
    {
        diamondDisplay.text = diamonds.ToString();
    }

    public void ChangeDiamonds(int amount) 
    {
        diamonds += amount;

        if (Sprinter.instance!=null) 
        {
            Sprinter.instance.UpdateSprinter();
        }
        return;
       
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string,bool>pair in data.diamondsCollected) 
        {
            if (pair.Value) 
            {
                diamonds++;
            }
        }
    }

    public void SaveData(ref GameData data)
    {
        
    }
}
