using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    public static DataPersistenceManager instance { get; set; }
    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileHandler dataHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of data persistence is found in scene. Destroying the newest one");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        
        DontDestroyOnLoad(this.gameObject);

        this.dataHandler = new FileHandler(Application.persistentDataPath, fileName);
    }

    public void NewGame() 
    {
        this.gameData = new GameData();
    }
    public void LoadGame() 
    {
        this.gameData = dataHandler.Load();

        if (this.gameData== null) 
        {
            Debug.Log("No data was found. A New Game needs to be started before data can be loaded");
            return;
        }

        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects) 
        {
            dataPersistenceObj.LoadData(gameData);
        }

    }

    public void SaveGame() 
    {
        if (this.gameData == null) 
        {
            Debug.LogWarning("No data was found. A new game to be started before data can be saved!");
            return;
        }
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(ref gameData);
        }

        dataHandler.Save(gameData);
    }


    [System.Obsolete]
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    { 
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void OnSceneUnloaded(Scene scene) 
    {
        SaveGame();
    }

    [System.Obsolete]
    private void OnEnable() 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    [System.Obsolete]
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    [System.Obsolete]
    private List <IDataPersistence> FindAllDataPersistenceObjects() 
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistence>();
        return new List<IDataPersistence>(dataPersistenceObjects);
    }
}
