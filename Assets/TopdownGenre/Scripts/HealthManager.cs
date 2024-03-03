using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int currentHealth;
    public int maxhealth;
    private EnemyAI enemy;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float force;

    private Transform playerMain;

    [SerializeField]
    private Transform enemyAI;

    [SerializeField]
    public GameObject popUpDamagePrefab;

    private bool hurtFlash;

    [SerializeField]
    private float flashTime = 0f;
    private float flashCountDown = 0f;


    // Color for the hurt flash (red)
    private Color hurtColor = new Color(1f, 0f, 0f);
    private SpriteRenderer playerSprite;
    private float moveDelay= 0.25f;

    [System.Obsolete]
    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        playerMain = FindObjectOfType<PlayerMovement>().transform;
        enemyAI = GetComponent<Transform>();
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
        playerSprite.color = color;
    }

    // Method to set sprite alpha
    private void SetSpriteAlpha(float alpha)
    {
        Color newColor = playerSprite.color;
        newColor.a = alpha;
        playerSprite.color = newColor;
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

        Vector2 distance = (enemyAI.transform.position - playerMain.transform.position).normalized;
        rb.AddForce(distance * force, ForceMode2D.Impulse);

        StartCoroutine(knockBackDelay(damageTaken));


        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
