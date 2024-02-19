using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxhealth;
    private EnemyAI enemy;

    private bool hurtFlash;

    [SerializeField]
    private float flashTime = 0f;
    private float flashCountDown = 0f;

    private SpriteRenderer enemySprite;

    // Color for the hurt flash (red)
    private Color hurtColor = new Color(1f, 0f, 0f);

    private void Start()
    {
        enemySprite = GetComponent<SpriteRenderer>();
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

    // Method to set sprite color
    private void SetSpriteColor(Color color)
    {
        enemySprite.color = color;
    }

    // Method to set sprite alpha
    private void SetSpriteAlpha(float alpha)
    {
        Color newColor = enemySprite.color;
        newColor.a = alpha;
        enemySprite.color = newColor;
    }

    public void damagePlayer(int damageTaken)
    {
        currentHealth -= damageTaken;
        hurtFlash = true;
        flashCountDown = flashTime;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
