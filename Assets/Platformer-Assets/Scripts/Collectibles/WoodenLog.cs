using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenLog : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private string woodenLog_id;
    public static WoodenLog instance;

    [SerializeField] private GameObject woodenLog;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        if (string.IsNullOrEmpty(woodenLog_id))
            GenerateGuidId();

        woodenLog.SetActive(false);
    }

    [ContextMenu("Generate Guild Id")]
    private void GenerateGuidId()
    {
        woodenLog_id = Guid.NewGuid().ToString();
    }

    public void UpdateLog()
    {
        if (KeyManager.instance.keys >= 3)
        {
            woodenLog.SetActive(true);
        }
    }

    public void LoadData(GameData data)
    {

        if (data.woodenLogPos.TryGetValue(woodenLog_id, out Vector3 savedPosition))
        {
            transform.position = savedPosition;

            if (data.CountOfKeysCollected() >= 3)
            {
                woodenLog.SetActive(true);
            }

        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.woodenLogPos.ContainsKey(woodenLog_id))
        {
            data.woodenLogPos.Remove(woodenLog_id);
        }
        data.woodenLogPos.Add(woodenLog_id, transform.position);
    }

}
