using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlots : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId = "";

    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    private Button saveSlotButton;

    AudioManager audioManager;

    [SerializeField] private TextMeshProUGUI percentageCompleteText;
    [SerializeField] private TextMeshProUGUI PrologueText;
    
    [Header("Clear Data Button")]
    [SerializeField] private Button clearButton;
    public bool hasData { get; private set; } = false;

    private void Awake()
    {
        saveSlotButton = this.GetComponent<Button>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void SetData(GameData data ) 
    {
        if (data == null) 
        {
            hasData = false;
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
            clearButton.gameObject.SetActive(false);
        }
        else
        {
            hasData = true;
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
            clearButton.gameObject.SetActive(true);

            percentageCompleteText.text = data.percentageCompleted() + "% COMPLETE";
            PrologueText.text = "PROLOGUE:5";
        }
    }

    public string GetProfileID() 
    {
        return this.profileId;
    }

    public void SetInteractable(bool interactable) 
    {
        saveSlotButton.interactable = interactable;
        clearButton.interactable = interactable;

    }

}

