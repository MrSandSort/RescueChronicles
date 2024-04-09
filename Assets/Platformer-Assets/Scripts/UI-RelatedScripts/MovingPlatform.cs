using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private string platform_id;

    [SerializeField]
    private Transform posA, posB;

    [SerializeField]
    private float Speed=0.5f;

    Vector2 targetPos;

    void Start()
    {
        targetPos = posB.position;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, posA.position) < .1f) targetPos= posB.position;
        if (Vector2.Distance(transform.position, posB.position) < .1f) targetPos = posA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed* Time.deltaTime);

    }


    [ContextMenu("Generate guid for Id")]
    private void GenerateGuidId()
    {
        platform_id = Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            other.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerExit2D(Collider2D another)
    {
        if (another.CompareTag("Player")) 
        {
            another.transform.SetParent(null);
        }
    }

    public void LoadData(GameData data)
    {
        if (data.platformPos.TryGetValue(platform_id, out Vector3 savedPosition))
        {
            transform.position = savedPosition;
        }
    }

    public void SaveData(ref GameData data)
    {
        if (data.platformPos.ContainsKey(platform_id))
        {
            data.platformPos[platform_id] = transform.position;
        }
        else
        {
            data.platformPos.Add(platform_id, transform.position);
        }
    }
}
