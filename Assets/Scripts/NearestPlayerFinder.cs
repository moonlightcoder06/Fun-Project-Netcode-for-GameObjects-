using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearestPlayerFinder : MonoBehaviour {

    public float searchRadius = 5f; // maximum search radius for players

    private Transform nearestPlayer; // reference to the nearest player

    [SerializeField]
    private AimAndShootNearsetPlayer aimAndShootScript;

    private void Update() {
        FindNearestPlayer();
    } // Update

    private void FindNearestPlayer() {

        // check if any players are within the search radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, searchRadius, LayerMask.GetMask("Player"));
        if (colliders.Length == 0) {
            nearestPlayer = null; // no players within range
            //print("no players within range");
            return;
        }

        // find the nearest player within range
        float nearestDistance = Mathf.Infinity;
        Transform newNearestPlayer = null;
        foreach (Collider2D collider in colliders) {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < nearestDistance) {
                nearestDistance = distance;
                newNearestPlayer = collider.transform;
            }
        }

        // update the nearest player reference
        nearestPlayer = newNearestPlayer;

        // do something with the nearest player
        if (nearestPlayer != null) {
            //Debug.Log("Nearest player: " + nearestPlayer.name);
            if (aimAndShootScript != null) { 
            aimAndShootScript.AimAtNearestPlayer(nearestPlayer, transform);
            }
        }

    } // FindNearestPlayer

    // draw search radius gizmo in scene view
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, searchRadius);
    } // OnDrawGizmosSelected

} // Class