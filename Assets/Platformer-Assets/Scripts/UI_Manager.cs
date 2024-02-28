using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public GameObject damageText;
    public GameObject healthText;

    public Canvas gameCanvas;

    [System.Obsolete]
    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();
    }

    private void OnEnable()
    {
        CharacterEvents.characterDamaged += damageTaken;
        CharacterEvents.characterHealed += healingDone;
    }

    private void OnDisable()
    {
        CharacterEvents.characterDamaged -= damageTaken;
        CharacterEvents.characterHealed -= healingDone;
    }

    public void damageTaken(GameObject character, int gotDamage) 
    {
        Vector3 spawn = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmp = Instantiate(damageText, spawn, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmp.text = gotDamage.ToString();
    }

    public void healingDone(GameObject character, int healDone) 
    {

        Vector3 spawn = Camera.main.WorldToScreenPoint(character.transform.position);
        TMP_Text tmp = Instantiate(healthText, spawn, Quaternion.identity, gameCanvas.transform).GetComponent<TMP_Text>();

        tmp.text = healDone.ToString();
    }
}
