using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3(0,75,0);

    TextMeshProUGUI Tmp;
    RectTransform transformText;

    public float fadeTime = 1f;
    private float timeElapsed= 0f;
    private Color sColor;

    private void Awake()
    {
        transformText = GetComponent<RectTransform>();
        Tmp = GetComponent<TextMeshProUGUI>();
        sColor = Tmp.color;
    }
    private void Update()
    {
        transformText.position += moveSpeed * Time.deltaTime;
        timeElapsed += Time.deltaTime;


        if (timeElapsed<= fadeTime) 
        {
            float newAlpha = sColor.a * (1 - (timeElapsed / fadeTime));
            Tmp.color = new Color(sColor.r, sColor.g, sColor.b, newAlpha);
        }
        else 
        {

            Destroy(gameObject);
        }
       
    }
}
