using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject PauseMenuPanel;
    AudioManager audioManager;
    void Start()
    {
        Time.timeScale = 1f;
    }

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused) 
            {
                Play();
            }
            else
            {
                Stop();
            
            }
        
        }
    }

    private void Stop()
    {
        audioManager.SFX_Play(audioManager.SelectBtn);
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Play()
    {
        audioManager.SFX_Play(audioManager.SelectBtn);
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenuButton() 
    {
        audioManager.SFX_Play(audioManager.SelectBtn);
        SceneManager.LoadScene(0);
    }

    public void SaveGameButton() 
    {
        audioManager.SFX_Play(audioManager.SelectBtn);
        DataPersistenceManager.instance.SaveGame();
        Play();
    }

    public void LoadGameButton() 
    {
        audioManager.SFX_Play(audioManager.SelectBtn);
        DataPersistenceManager.instance.LoadGame();
    }


}
