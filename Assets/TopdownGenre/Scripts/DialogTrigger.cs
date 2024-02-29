using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    public void StartDialogue()
    {
        Object.FindAnyObjectByType<DialogManager>().OpenDialogue(messages, actors);
    }

    public void EndDialogue() 
    {
        Object.FindAnyObjectByType<DialogManager>().CloseDialogue();
    }
}
[System.Serializable]
public class Message {
    public int actorId;
    public string message;
}

[System.Serializable]
public class Actor
{
    public string name;
    public Sprite sprite;
}