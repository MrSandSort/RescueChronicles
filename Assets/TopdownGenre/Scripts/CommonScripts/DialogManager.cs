using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    public Image actorImage;
    public TextMeshProUGUI actorName;
    public TextMeshProUGUI messageText;
    public RectTransform backgroundBox;

    Message[] currentMessages;
    Actor[] currentActors;
    int activeMessage = 0;
    public static bool isActive = false;


    public void OpenDialogue(Message[] messages, Actor[] actors) {
;
        currentMessages = messages;
        currentActors = actors;
        activeMessage = 0;
        isActive = true;

        Debug.Log("Started Conversation! Loaded messages:" + messages.Length);
        DisplayMessages();
        backgroundBox.LeanScale(Vector3.one, 0.5f);

    }


    void DisplayMessages() {

        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;
        Actor actorToDisplay = currentActors[messageToDisplay.actorId];
        actorName.text = actorToDisplay.name;
        actorImage.sprite = actorToDisplay.sprite;

        AnimateTextColor();

    }

    public void NextMessages()
    {
        activeMessage++;
        if (activeMessage < currentMessages.Length)
        {
            DisplayMessages();
        }
        else {

            CloseDialogue();
        }
    }


    void AnimateTextColor() {
        LeanTween.textAlpha(messageText.rectTransform,0,0);
        LeanTween.textAlpha(messageText.rectTransform,1,0.5f);
    }


    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;

    }

     public void CloseDialogue()
    {
        backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
        isActive = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && isActive== true) 
        {
            NextMessages();
        }
        
    }
}
