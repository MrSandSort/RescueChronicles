using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    private EnemyDealDamage enemyHealth;
    public Slider healthBar;
    public Vector3 offset;

    [System.Obsolete]
    void Start()
    {
        enemyHealth = FindObjectOfType<EnemyDealDamage>();

    }
    void Update()
    {
        healthBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position+ offset);
        healthBar.maxValue = enemyHealth.maxhealth;
        healthBar.value = enemyHealth.currentHealth;

    }
}
