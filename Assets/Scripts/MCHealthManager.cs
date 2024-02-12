using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCHealthManager : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public Slider healthBar;

    void Start()
    {
        maxHealth = health;

    }
    void Update()
    {
        healthBar.value = Mathf.Clamp(health / maxHealth, 0, 1);

    }
}
