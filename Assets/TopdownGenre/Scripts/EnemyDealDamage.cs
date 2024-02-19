using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDealDamage : MonoBehaviour
{
    public int currentHealth;
    public int maxhealth;

    private bool hurtFlash;

    [SerializeField]
    private float flashTime = 0f;
    private float flashTimer = 0f;

    private SpriteRenderer spriteEnemy;

    // Color for the hurt flash (red)
    private Color hurtColor = new Color(1f, 0f, 0f);

    private void Start()
    {
        spriteEnemy = GetComponent<SpriteRenderer>();
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

    // Method to set sprite color
    private void SetSpriteColor(Color color)
    {
        spriteEnemy.color = color;
    }

    // Method to set sprite alpha
    private void SetSpriteAlpha(float alpha)
    {
        Color newColor = spriteEnemy.color;
        newColor.a = alpha;
        spriteEnemy.color = newColor;
    }

    public void DamageEnemy(int damageTaken)
    {
        currentHealth -= damageTaken;
        hurtFlash = true;
        flashTimer = flashTime;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
