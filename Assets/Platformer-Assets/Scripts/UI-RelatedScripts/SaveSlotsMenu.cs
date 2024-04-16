using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSlotsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private mainMenu mainMenu;
    private SaveSlots[] saveSlots;

    AudioManager audioManager;

    private bool isLoadingGame = true;

    [Header("Menu Button")]
    [SerializeField] private Button backButton;

    [Header("Confirmation Tab")]
    [SerializeField] private ConfirmationTabs confirmationTab;

    private void Awake()
    {
        saveSlots = this.GetComponentsInChildren<SaveSlots>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void OnClearClicked(SaveSlots saveSlot) 
    {
        audioManager.SFX_Play(audioManager.SelectBtn);
        DisableMenuButtons();

        confirmationTab.ActivateMenu
            
            ("Do you want to clear slots?", 
            () => 
            {
                DataPersistenceManager.instance.DeleteProfileData(saveSlot.GetProfileID());
                ActivateMenu(isLoadingGame);
            },
            ()=>
            {
                ActivateMenu(isLoadingGame);
            });
     
    }
    public void OnBackClicked() 
    {
        audioManager.SFX_Play(audioManager.SelectBtn);
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }
    public void ActivateMenu(bool isLoadingGame) 
    {
        this.gameObject.SetActive(true);
        this.isLoadingGame = isLoadingGame;

        Dictionary<string, GameData> profilesGameData = DataPersistenceManager.instance.GetAllProfileGameData();

        backButton.interactable = true;

        GameObject firstSelected = backButton.gameObject;
        audioManager.SFX_Play(audioManager.SelectBtn);


        foreach (SaveSlots saveSlot in saveSlots) 
        {
            GameData profileData = null;
            profilesGameData.TryGetValue(saveSlot.GetProfileID(), out profileData);
            saveSlot.SetData(profileData);

            if (profileData == null && isLoadingGame) 
            {
                saveSlot.SetInteractable(false);
            }
            else 
            {
                saveSlot.SetInteractable(true);
                audioManager.SFX_Play(audioManager.SelectBtn);

                if (firstSelected.Equals(backButton.gameObject)) 
                {
                    firstSelected = saveSlot.gameObject;
                    
                }
            }

        }
        Button firstSelectedButton = firstSelected.GetComponent<Button>();
        this.SetFirstSelected(firstSelectedButton);
    }

    public void OnSaveSlotClicked(SaveSlots saveSlot)
    {
        audioManager.SFX_Play(audioManager.SelectBtn);
        DisableMenuButtons();
        
        if (isLoadingGame)
        {
            DataPersistenceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileID());
            SaveGameAndLoadScene();
        }
        else if (saveSlot.hasData)
        {
            confirmationTab.ActivateMenu("Are you sure, you want to remove slot", 
            () => 
            {
                audioManager.SFX_Play(audioManager.SelectBtn);
                DataPersistenceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileID());
                DataPersistenceManager.instance.NewGame();
                SaveGameAndLoadScene();
            }, 
            () => 
            {
                audioManager.SFX_Play(audioManager.SelectBtn);
                this.ActivateMenu(isLoadingGame);
            });
        }
        else 
        {
            audioManager.SFX_Play(audioManager.SelectBtn);
            DataPersistenceManager.instance.ChangeSelectedProfileID(saveSlot.GetProfileID());
            DataPersistenceManager.instance.NewGame();
            SaveGameAndLoadScene();
        }
    }

    private void SaveGameAndLoadScene() 
    {
        DataPersistenceManager.instance.SaveGame();
        SceneManager.LoadSceneAsync("Level-3 Scene");
    }
    public void DeactivateMenu() 
    {
        this.gameObject.SetActive(false);
    }

    private void DisableMenuButtons() 
    {
        foreach (SaveSlots saveslot in saveSlots) 
        {
            saveslot.SetInteractable(false);
        }
        backButton.interactable = false;
    }
}
