using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GameData 
{
   public long lastUpdated;
   public Vector3 playerPos;
   public int Health;
   public int MaxHealth;
   public SerializableDictionary<string, Vector3> sprinterPos;
   public SerializableDictionary<string, Vector3> platformPos; 
   public SerializableDictionary<string, bool> diamondsCollected;
   public SerializableDictionary<string, bool> keysCollected;
   public SerializableDictionary<string, bool> enemyDeafeated;
   public SerializableDictionary<string, Vector3> woodenLogPos;
   public SerializableDictionary<string, bool> checkPoint;

    public GameData()
   {
       MaxHealth = 100;  
       Health = MaxHealth;
       sprinterPos = new SerializableDictionary<string, Vector3>();
       checkPoint = new SerializableDictionary<string, bool>(); 
       keysCollected = new SerializableDictionary<string, bool>();
       woodenLogPos = new SerializableDictionary<string, Vector3>();
       playerPos = Vector3.zero;
       platformPos = new SerializableDictionary<string, Vector3>();  
       diamondsCollected = new SerializableDictionary<string, bool>();
       enemyDeafeated = new SerializableDictionary<string, bool>();
   }
   
   public int percentageCompleted() 
   {
        int totalDiamondsCollected = 0;
        
        foreach (bool collected in diamondsCollected.Values) 
        {
            if (collected) 
            {
                totalDiamondsCollected++;
            }
        }

        int percentageCompleted = 0;

        if (diamondsCollected.Count!=0) 
        {
            percentageCompleted = (totalDiamondsCollected * 100 / diamondsCollected.Count);
        }
        return percentageCompleted;
   }

   public int CountOfdiamondsCollected() 
   {
        int totalDiamondsCollected = 0;

        foreach (bool collected in diamondsCollected.Values)
        {
            if (collected)
            {
                totalDiamondsCollected++;
            }
        }
        return totalDiamondsCollected;
    }

    public int CountOfKeysCollected() 
    {
        int totalKeysCollected = 0;

        foreach (bool collected in keysCollected.Values)
        {
            if (collected)
            {
                totalKeysCollected++;
            }
        }

        return totalKeysCollected;
    }
}
