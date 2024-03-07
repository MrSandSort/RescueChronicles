using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_Platform : MonoBehaviour
{
    public Slider sliderHealth;
    public TMP_Text healthbarText;
    Damageable playerDamageable;
  

     private void Awake()
     {
         GameObject player = GameObject.FindGameObjectWithTag("Player");

         if(player != null) 
         {
             Debug.Log("No player is found with the tag.");

         }
         playerDamageable = player.GetComponent<Damageable>();
     }
     void Start()
     {
         sliderHealth.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
         healthbarText.text = "Health " + playerDamageable.Health + "/" + playerDamageable.MaxHealth;
     }

     private void OnEnable()
     {
         playerDamageable.healthChange.AddListener(OnPlayerHealthChanged);
     }

     private void OnDisable()
     {
         playerDamageable.healthChange.RemoveListener(OnPlayerHealthChanged);
     }
     private float CalculateSliderPercentage(float curHlth, float mHlth) 
     {
         return (curHlth / mHlth)* 100;
     }

     private void OnPlayerHealthChanged(int nHlth,int mHlth)
     { 
         sliderHealth.value = CalculateSliderPercentage(nHlth, mHlth);
         healthbarText.text = "Health " + nHlth + "/" + mHlth;
     }
 }