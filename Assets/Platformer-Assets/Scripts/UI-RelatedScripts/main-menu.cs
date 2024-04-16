using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : Menu
{
    AudioManager audioManager;

    [Header("Menu Navigation")]
    [SerializeField] private SaveSlotsMenu saveSlotsMen;
    [SerializeField] private TutorialsMenu tutorialPanel;

    [Header("Menu Buttons")]

    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button tutorialButton;

    private void Start()
    {
        DisableButtonDependingOnData();
    }
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();   
    }
    private void DisableButtonDependingOnData() 
    {
        if (!DataPersistenceManager.instance.hasGameData())
        {
            continueGameButton.interactable = false;
            loadGameButton.interactable = false;
        }
    }
    public void OnNewGameClicked() 
   {
        audioManager.SFX_Play(audioManager.SelectBtn);
        saveSlotsMen.ActivateMenu(false);
        this.DeactivateMenu();
   }

    public void OnLoadGameClicked() 
    {
        audioManager.SFX_Play(audioManager.SelectBtn);
        saveSlotsMen.ActivateMenu(true);
        this.DeactivateMenu();
    }
   public void OnContinueGameClicked() 
   {
        audioManager.SFX_Play(audioManager.SelectBtn);
        DisableMenuButtons();
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Level-3 Scene");
   }


    private void DisableMenuButtons()
    {
        if (newGameButton != null)
        {
            newGameButton.interactable = false;
        }

        if (continueGameButton != null)
        {
            continueGameButton.interactable = false;
        }
    }

    public void ActivateMenu() 
    {
        this.gameObject.SetActive(true);
        DisableButtonDependingOnData();
    }

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
    }
}
