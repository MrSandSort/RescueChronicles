using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MCHealthManager : MonoBehaviour
{
    private HealthManager healthMan;
    public Slider healthBar;

    [System.Obsolete]
    void Start()
    {
         healthMan= FindObjectOfType<HealthManager>();

    }
    void Update()
    {
        healthBar.maxValue = healthMan.maxhealth;
        healthBar.value = healthMan.currentHealth;

    }
}
