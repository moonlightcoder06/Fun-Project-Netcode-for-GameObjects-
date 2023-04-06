using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour
{

    public Image healthBar;
    public float healthAmount = 100f;
    public SpriteRenderer Enemy;

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

        Enemy.color = new Color(0.7f, 0.1f, 0.3f, healthAmount/100f);
        StartCoroutine(RestoreColor());
    }

    IEnumerator RestoreColor() {
        yield return new WaitForSeconds(0.15f);
        Enemy.color = new Color(1f, 1f, 1f, healthAmount / 100f);
    }

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