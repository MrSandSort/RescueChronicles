using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     DataPersistenceManager  dataPersist;
    private void Awake()
    {
        dataPersist = GetComponent<DataPersistenceManager>();
    }
    public void NewGame()
    {
       SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex+1);
       
    }

    public void LoadGame() 
    {  
     dataPersist.LoadGame();
    }
}
