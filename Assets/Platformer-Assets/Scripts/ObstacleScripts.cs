using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScripts : MonoBehaviour
{
    Damageable damageable;
    private void OnCollisionEnter(Collision waterSprites)
    {
        if (waterSprites.gameObject.tag=="Water") 
        {
            damageable.Health = 0;
        
        }
    }
}
