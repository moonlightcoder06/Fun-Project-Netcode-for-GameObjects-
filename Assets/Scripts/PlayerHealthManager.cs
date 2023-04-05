using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthManager : MonoBehaviour
{
    public Image healthBar;
    public float healthAmount = 100f;

    // Update is called once per frame
    void Update()
    {
        if (healthAmount <= 0) {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            TakeDamage(20f);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            Heal(5f);
        }
    } // Update

    public void TakeDamage(float damage) { 
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    } // TakeDamage

    public void Heal(float healingAmount) {
        healthAmount += healingAmount;
        healthAmount = Mathf.Clamp(healthAmount, 0, 100);

        healthBar.fillAmount = healthAmount / 100f;
    } // Heal

} // Class