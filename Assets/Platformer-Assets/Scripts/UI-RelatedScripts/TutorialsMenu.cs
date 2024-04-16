using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialsMenu : Menu
{
    [Header("Menu Navigation")]
    [SerializeField] private mainMenu mainMenu;

    [Header("Menu Button")]
    [SerializeField] private Button backButton;

    public void DeactivateMenu()
    {
        this.gameObject.SetActive(false);
        mainMenu.ActivateMenu();
    }

    public void ActivateMenu() 
    {
        this.gameObject.SetActive(true);
        mainMenu.DeactivateMenu();

        backButton.interactable = true;
        GameObject firstSelected = backButton.gameObject;

        Button firstSelectedButton = firstSelected.GetComponent<Button>();
        this.SetFirstSelected(firstSelectedButton);
    }
    public void OnTutorialGameClicked()
    {
        backButton.interactable = false;
        this.ActivateMenu();
        mainMenu.DeactivateMenu();
    }

    public void OnBackButtonClicked()
    {
        mainMenu.ActivateMenu();
        this.DeactivateMenu();
    }

}
