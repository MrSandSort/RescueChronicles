using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCHealthManager : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = Mathf.Clamp(health / maxHealth, 0, 1);
    }
}
