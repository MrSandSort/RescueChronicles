using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    [Header("Menu Buttons")]

    [SerializeField] private Button newGameButton;
    [SerializeField] private Button continueGameButton;
   public void OnNewGameClicked() 
   {
        DisableMenuButtons();
        DataPersistenceManager.instance.NewGame();
        SceneManager.LoadSceneAsync("Level-5 Scene");
   }

   public void OnContinueGameClicked() 
   {
        DisableMenuButtons();
        SceneManager.LoadSceneAsync("Level-5 Scene");
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
}
