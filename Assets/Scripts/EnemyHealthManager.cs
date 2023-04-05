using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{

    public Image healthBar;
    public float healthAmount = 100f;

    public GameObject healthCanvas;

    void Update()
    {
        if (healthAmount <= 0) {
            SelfDestruct();
        }
    } // Update

    public void TakeDamage(float damage) { 
        healthAmount -= damage;
        healthBar.fillAmount = healthAmount / 100f;
    } // TakeDamage

    void SelfDestruct() {
        if (transform.parent != null) {
            // Parent GameObject exists
            Destroy(transform.parent.gameObject);
        } else {
            // Parent GameObject does not exist
            Destroy(gameObject);
        }
        Destroy(healthCanvas);
    } // SelfDestruct

} // Class