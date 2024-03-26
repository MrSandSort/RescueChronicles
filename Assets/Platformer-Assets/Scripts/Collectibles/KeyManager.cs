using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager instance;
    private int keys;

    [SerializeField] private TMP_Text keyDisplay;
    [SerializeField] private GameObject woodenLog;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            woodenLog.SetActive(false);
        }
    }


    private void OnGUI()
    {
        keyDisplay.text = keys.ToString();
    }

    public void AddKeys(int amount)
    {
        keys += amount;
        UpdateLogActivation();
    }

    private void UpdateLogActivation()
    {
        bool activateLog = keys >= 5;
        woodenLog.SetActive(activateLog);

    }
}
