using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttraction: MonoBehaviour {
    public float attractionForce = 20.0f;
    public float maxDistance = 5.0f;

    private GameObject player;

    void Start() {
        player = GameObject.FindWithTag("Player");
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
    }
}
