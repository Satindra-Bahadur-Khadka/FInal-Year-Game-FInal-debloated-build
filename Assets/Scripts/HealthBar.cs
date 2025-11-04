using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBarSlider;
    public TMP_Text healthBarText;



    Damageable playerDamageable;


    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player == null)
        {
            Debug.LogError("Player not found in the scene. Make sure the player has the 'Player' tag.");
            
        }  
        playerDamageable = player.GetComponent<Damageable>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        healthBarSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
        healthBarText.text = "HP" + playerDamageable.Health + "/" + playerDamageable.MaxHealth;
    }


    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);


    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }



    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthBarSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP" + newHealth + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
