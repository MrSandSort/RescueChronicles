using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[System.Serializable]
public class GameData 
{
   public Vector3 playerPos;
   public SerializableDictionary<string, Vector3> platformPos; 
   public SerializableDictionary<string, bool> diamondsCollected;
   public SerializableDictionary<string, bool> enemyDeafeated;

   public GameData()
   {
       playerPos = Vector3.zero;
       platformPos = new SerializableDictionary<string, Vector3>();  
       diamondsCollected = new SerializableDictionary<string, bool>();
       enemyDeafeated = new SerializableDictionary<string, bool>();

   }
}
