using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Npc : MonoBehaviour
{
    public DialogTrigger trigger;
    public DialogManager manager;
    public Rigidbody2D rb;
    public int activeCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        manager = GetComponent<DialogManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            trigger.StartDialogue();
            activeCount ++;
        }
       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (activeCount > 1) 
        {
            Destroy(gameObject);
            trigger.EndDialogue();
        }
    }


}
