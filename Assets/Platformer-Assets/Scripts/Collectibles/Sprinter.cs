using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Sprinter : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private string sprinter_id;
    public static Sprinter instance;

    [SerializeField] private GameObject sprinter;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }

        if (string.IsNullOrEmpty(sprinter_id))
            GenerateGuidId();

        sprinter.SetActive(false);
    }

    [ContextMenu("Generate Guild Id")]
    private void GenerateGuidId()
    {
        sprinter_id = Guid.NewGuid().ToString();
    }

    public void UpdateSprinter()
    {
        if (DiamondManager.instance.diamonds >= 4)
        {
            sprinter.SetActive(true);
        }
    }
    public void LoadData(GameData data)
    {

        if (data.sprinterPos.TryGetValue(sprinter_id, out Vector3 savedPosition))
        {
            transform.position = savedPosition;

            if (data.CountOfdiamondsCollected() >= 4)
            {
                sprinter.SetActive(true);
            }

        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.sprinterPos.ContainsKey(sprinter_id))
        {
            data.sprinterPos.Remove(sprinter_id);
        }
        data.sprinterPos.Add(sprinter_id, transform.position);
    }
}
