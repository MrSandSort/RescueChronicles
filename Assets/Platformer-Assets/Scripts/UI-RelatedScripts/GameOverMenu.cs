using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject player;

    PauseMenu pauseMenu;
    Damageable damageable;

    private void Awake()
    {
        pauseMenu = GetComponent<PauseMenu>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            Debug.Log("No player is found with the tag.");

        }

        damageable = player.GetComponent<Damageable>();
    }

    private void Update()
    {
        if (!damageable.IsAlive) 
        {
            ShowGameOverScreen();
        }
    }
    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; 
    }
    public void ReturnToMainMenu()
    {
        pauseMenu.MainMenuButton();
    }

    public void RestartGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        if (!damageable.IsAlive) 
        {
            data.Health = data.MaxHealth;
        }
        return;
        
    }
}
