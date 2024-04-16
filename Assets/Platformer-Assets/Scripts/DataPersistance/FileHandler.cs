using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public FileHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load(string profileId)
    {
        if (profileId == null) 
        {
            return null;
        }
        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);
        GameData LoadedData = null;

        if (File.Exists(fullPath)) 
        {
            try
            {
                string dataToLoad="";
                
                using (FileStream stream= new FileStream(fullPath,FileMode.Open)) 
                {
                    using (StreamReader reader= new StreamReader(stream)) 
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                LoadedData = JsonUtility.FromJson<GameData>(dataToLoad);

            }
            catch (Exception e) 
            {
                Debug.LogError("Error Occured while trying to save data to file" + fullPath + "\n" + e);
            }
        }
        return LoadedData;
    }


    public Dictionary<string, GameData> LoadAllProfile()
    {
        Dictionary<string, GameData> profileDictionary = new Dictionary<string, GameData>();
        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(dataDirPath).EnumerateDirectories();

        foreach (DirectoryInfo dirInfo in dirInfos) 
        {
            string profileId = dirInfo.Name;
            string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);

            if (!File.Exists(fullPath)) 
            {
                Debug.LogError("Skipping Directory"+ profileId);  
                continue;
            }
            GameData profileData = Load(profileId);

            if (profileData!= null) 
            {
                profileDictionary.Add(profileId, profileData);
            }
            else 
            {
                Debug.LogError("Tried to load profile but something went wrong"+ profileId);
            }
        }

        return profileDictionary;
    }


    public void Save(GameData data, string profileId)
    {
        if (profileId == null) 
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath, profileId, dataFileName);

        try 
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using (FileStream stream = new FileStream(fullPath,FileMode.Create)) 
            {
                using (StreamWriter writer = new StreamWriter(stream)) 
                {
                    writer.Write(dataToStore);
                }
            }
        } 
        catch (Exception e) 
        {
            Debug.LogError("Error Occured while trying to save data to file"+ fullPath+ "\n"+ e);
        }
    }

    public string GetMostRecentUpdatedProfileId() 
    {
        string mostRecentProfileId = null;
        Dictionary<string, GameData> profilesGameData = LoadAllProfile();
        foreach (KeyValuePair <string,GameData> pair in profilesGameData) 
        {
            string profileId = pair.Key;
            GameData gameData = pair.Value;

            if(gameData== null) 
            {
                continue;
            }
            if(mostRecentProfileId== null) 
            {
                mostRecentProfileId = profileId;
            }
            else 
            {
                DateTime mostRecentDateTime = DateTime.FromBinary(profilesGameData[mostRecentProfileId].lastUpdated);
                DateTime newDateTime = DateTime.FromBinary(gameData.lastUpdated);

                if (newDateTime > mostRecentDateTime) 
                {
                    mostRecentProfileId = profileId;
                }
            }
        }

        return mostRecentProfileId;
    }

    public void Delete(string profileId) 
    {
        if (profileId== null) 
        {
            return;
        }

        string fullPath = Path.Combine(dataDirPath,profileId, dataFileName);

        try 
        {
            if (File.Exists(fullPath)) 
            {
                Directory.Delete(Path.GetDirectoryName(fullPath), true);
            }
            else 
            {
                Debug.LogWarning("Tried to delete but data was not found!");
            }
        }
        catch (Exception e) 
        {
            Debug.LogError("Failed to delete data with" + profileId +"due to" +e);
        }
    }
}
