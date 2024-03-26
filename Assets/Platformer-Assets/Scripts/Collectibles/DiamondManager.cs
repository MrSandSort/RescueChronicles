using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DiamondManager : MonoBehaviour
{
    public static DiamondManager instance;
    private int diamonds;

    [SerializeField] private TMP_Text diamondDisplay;
    [SerializeField] private GameObject sprinter;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            sprinter.SetActive(false);
        }
    }


    private void OnGUI()
    {
        diamondDisplay.text = diamonds.ToString();
    }

    public void ChangeDiamonds(int amount) 
    {

        diamonds += amount;
        UpdateSprinterActivation();
    }

    private void UpdateSprinterActivation()
    {
        bool activateSprinters = diamonds >= 5;
        sprinter.SetActive(activateSprinters);

    }
}
