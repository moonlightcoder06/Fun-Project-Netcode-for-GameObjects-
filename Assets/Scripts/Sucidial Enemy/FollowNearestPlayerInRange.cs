using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowNearestPlayerInRange : MonoBehaviour
{

    public float detectionRange = 10f;  // the range within which the enemy can detect players
    public float moveSpeed = 5f;        // the speed at which the enemy moves towards the nearest player

    private Transform target;           // the transform of the player that the enemy is currently targeting

    void Update() {
        // if we use this logic then it any player comes in its range then the enemy will follow only that player and irrespective of other player position
        //if (target == null || Vector2.Distance(transform.position, target.position) > detectionRange) {
        //    FindNearestPlayer();
        //}

        // if there is a target, move towards it at the specified speed
        if (target != null) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    } // Update

    private void FixedUpdate() {
        FindNearestPlayer();
        //print(target.name);
    } // FixedUpdate

    void FindNearestPlayer() {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, detectionRange, LayerMask.GetMask("Player"));

        // Check if colliders array is empty
        if (colliders.Length == 0) {
            target = null;
            return;
        }

        Transform nearestPlayer = null;
        float nearestDistance = Mathf.Infinity;

        // Loop through all colliders and find the nearest player
        foreach (Collider2D collider in colliders) {
            // check if the collider is tagged as "Player"
            if (collider.tag == "Player") {
                // calculate the distance between the enemy and the player
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                // if the distance is less than the current nearest distance, update the nearest player and distance
                if (distance < nearestDistance) {
                    nearestPlayer = collider.transform;
                    nearestDistance = distance;
                }
            }
        }

        // Set the nearest player as the target
        target = nearestPlayer;

    } // FindNearestPlayer

    void OnDrawGizmos() {
        // Draw a wire sphere to visualize the detection range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    } // OnDrawGizmos

} // Class