using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxhealth;

    [SerializeField]
    private Rigidbody2D rb;


    [SerializeField]
    public GameObject popUpDamagePrefab;

    private bool hurtFlash;

    [SerializeField]
    private float flashTime = 0f;
    private float flashCountDown = 0f;


    private Color hurtColor = new Color(1f, 0f, 0f);
    private SpriteRenderer playerSprite;

    [System.Obsolete]
    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (hurtFlash)
        {
            if (flashCountDown > flashTime * .99f)
            {
                SetSpriteAlpha(0f); // Set alpha to 0
            }
            else if (flashCountDown > flashTime * .75f)
            {
                SetSpriteColor(hurtColor); // Set sprite color to red with full alpha
            }
            else if (flashCountDown > flashTime * .50f)
            {
                SetSpriteAlpha(0f); // Set alpha to 0
            }
            else if (flashCountDown > flashTime * .25f)
            {
                SetSpriteColor(hurtColor); // Set sprite color to red with full alpha
            }
            else if (flashCountDown > 0f)
            {
                SetSpriteAlpha(0f); // Set alpha to 0
            }
            else
            {
                SetSpriteColor(Color.white); // Reset sprite color to white with full alpha
                hurtFlash = false;
            }
            flashCountDown -= Time.deltaTime;
        }
    }

    private void SetSpriteColor(Color color)
    {
        playerSprite.color = color;
    }

    private void SetSpriteAlpha(float alpha)
    {
        Color newColor = playerSprite.color;
        newColor.a = alpha;
        playerSprite.color = newColor;
    }

    void showDamage(string text)
    {
        if (popUpDamagePrefab)
        {
            GameObject prefab = Instantiate(popUpDamagePrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }

    }

    public void damagePlayer(int damageTaken)
    {
        currentHealth -= damageTaken;
        hurtFlash = true;
        flashCountDown = flashTime;

        showDamage(damageTaken.ToString());

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
