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

    private Rigidbody2D playerRB;
    private Animator playerAnim; 


    public void OpenDialogue(Message[] messages, Actor[] actors) {
;
        playerRB.velocity = Vector2.zero;
        playerAnim.SetFloat("X", 0);
        playerAnim.SetFloat("Y", 0);

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

            Debug.Log("Conversation ended!");
            backgroundBox.LeanScale(Vector3.zero, 0.5f).setEaseInOutExpo();
            isActive = false;


        }
    }


    void AnimateTextColor() {
        LeanTween.textAlpha(messageText.rectTransform,0,0);
        LeanTween.textAlpha(messageText.rectTransform,1,0.5f);
    }


    void Start()
    {
        backgroundBox.transform.localScale = Vector3.zero;

        GameObject rb = GameObject.FindGameObjectWithTag("Player");
        if (rb != null)
        {
            playerRB = rb.GetComponent<Rigidbody2D>();
        }

        GameObject animator = GameObject.FindGameObjectWithTag("Player");
        if (animator != null)
        {
            playerAnim = animator.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && isActive== true) 
        {
            NextMessages();
        
        }
        
    }
}
