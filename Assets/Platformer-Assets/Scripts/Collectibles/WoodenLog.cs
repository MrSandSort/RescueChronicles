using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenLog : MonoBehaviour
{
    public static WoodenLog instance;
    [SerializeField] private GameObject woodenLog;


    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            woodenLog.SetActive(false);

        }
    }
    public void UpdateWoodenLog()
    {
        if (KeyManager.instance.keys>=4 )
        {
            woodenLog.SetActive(true);
        }
    }
}
