using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
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
        if (damageable.Health <= 0) 
        {
            ShowGameOverScreen();
        }
    }
    public void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; 
    }

    public void RestartGame()
    {
        pauseMenu.RestartGame();
    }
    public void ReturnToMainMenu()
    {
        pauseMenu.MainMenuButton();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
