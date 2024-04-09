using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondManager : MonoBehaviour
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
    }

   
}
