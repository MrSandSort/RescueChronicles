using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sprinter : MonoBehaviour
{
    public static Sprinter instance;
    [SerializeField] private GameObject sprinter;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            sprinter.SetActive(false);
        }
    }

    public void UpdateSprinter()
    {
        if (DiamondManager.instance.diamonds == 4) 
        {
            sprinter.SetActive(true);
        }
    }
}
