using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttraction: MonoBehaviour {
    public float attractionForce = 20.0f;
    public float maxDistance = 5.0f;
    public int damagePerSecond = 10;

    private GameObject player;
    private GameObject playerHealthManager;

    void Start() {

        player = GameObject.FindWithTag("Player");
        playerHealthManager = GameObject.FindWithTag("HealthManager");
    }

    void Update() {

        // Attract the player towards the GameObject
        Vector3 direction = transform.position - player.transform.position;
        float distance = direction.magnitude;

        if (distance < maxDistance) {
            float forceMagnitude = attractionForce * (1.0f - (distance / maxDistance));
            Vector3 force = direction.normalized * forceMagnitude;
            player.GetComponent<Rigidbody2D>().AddForce(force);
        }

        // Check if player is colliding with the GameObject
        if (IsCollidingWithPlayer()) {
            // Reduce player's health by damagePerSecond
            playerHealthManager.GetComponent<PlayerHealthManager>().TakeDamage(damagePerSecond * Time.deltaTime);
        }
    }


    bool IsCollidingWithPlayer() {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2);

        foreach (Collider2D collider in colliders) {
            if (collider.CompareTag("Player")) {
                return true;
            }
        }

        return false;
    }
}
