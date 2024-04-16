using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyManager : MonoBehaviour, IDataPersistence
{
    public static KeyManager instance;
    public int keys;

    [SerializeField] private TMP_Text keyDisplay;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    private void OnGUI()
    {
        keyDisplay.text = keys.ToString();
    }

    public void AddKeys(int amount)
    {
        keys += amount;
        WoodenLog.instance.UpdateLog();
    }

    public void LoadData(GameData data)
    {
        foreach (KeyValuePair<string, bool> pair in data.keysCollected)
        {
            if (pair.Value)
            {
                keys++;
            }
        }
    }

    public void SaveData(ref GameData data)
    {

    }
}

