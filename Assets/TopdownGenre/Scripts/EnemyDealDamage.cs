using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    public int currentHealth;
    public int maxhealth;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float force;

    private Transform send;

    [SerializeField]
    private Transform receive;

    [SerializeField]
    public GameObject popUpDamagePrefab;

    private bool hurtFlash;

    [SerializeField]
    private float flashTime = 0f;
    private float flashTimer = 0f;

    private SpriteRenderer spriteEnemy;
    private Color hurtColor = new Color(1f, 0f, 0f);
    private float moveDelay=0.20f;

    [System.Obsolete]
    private void Start()
    {
        spriteEnemy = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        send = FindObjectOfType<PlayerMovement>().transform;
        receive = GetComponent<Transform>();
        
    }

    private void Update()
    {
        if (hurtFlash)
        {
            if (flashTimer > flashTime * .99f)
            {
                SetSpriteAlpha(0f); // Set alpha to 0
            }
            else if (flashTimer > flashTime * .75f)
            {
                SetSpriteColor(hurtColor); // Set sprite color to red with full alpha
            }
            else if (flashTimer > flashTime * .50f)
            {
                SetSpriteAlpha(0f); // Set alpha to 0
            }
            else if (flashTimer > flashTime * .25f)
            {
                SetSpriteColor(hurtColor); // Set sprite color to red with full alpha
            }
            else if (flashTimer > 0f)
            {
                SetSpriteAlpha(0f); // Set alpha to 0
            }
            else
            {
                SetSpriteColor(Color.white); // Reset sprite color to white with full alpha
                hurtFlash = false;
            }
            flashTimer -= Time.deltaTime;
        }
    }

    private void SetSpriteColor(Color color)
    {
        spriteEnemy.color = color;
    }

    private void SetSpriteAlpha(float alpha)
    {
        Color newColor = spriteEnemy.color;
        newColor.a = alpha;
        spriteEnemy.color = newColor;
    }

    IEnumerator knockBackDelay(int damage) 
    {
        yield return new WaitForSeconds(moveDelay);
        rb.velocity = Vector2.zero;

        if (rb.velocity == Vector2.zero) 
        {
            showDamage(damage.ToString());
        }

    }

    public void DamageEnemy(int damageTaken)
    {
        currentHealth -= damageTaken;
        hurtFlash = true;
        flashTimer = flashTime;

        Vector2 distance = (receive.transform.position - send.transform.position).normalized;
        rb.AddForce(distance * force, ForceMode2D.Impulse);

        StartCoroutine(knockBackDelay(damageTaken));

        if (currentHealth <= 0)
        {
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        float alpha = 1f;

        while (alpha > 0)
        {
            alpha -= Time.deltaTime;
            SetSpriteAlpha(alpha);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    void showDamage(string text) 
    {
        if (popUpDamagePrefab) 
        {
            GameObject prefab = Instantiate(popUpDamagePrefab,transform.position,Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    
    }
}
