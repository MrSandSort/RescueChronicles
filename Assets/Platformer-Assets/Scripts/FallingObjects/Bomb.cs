using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;

    public int index;

    [SerializeField]
    private float Speed = 2f;


    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[index].position, Speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, points[index].position)< .1f)
        {
            index++;
        }

        if(index== points.Length) 
        {
            index = 0;
        }
    }
}
