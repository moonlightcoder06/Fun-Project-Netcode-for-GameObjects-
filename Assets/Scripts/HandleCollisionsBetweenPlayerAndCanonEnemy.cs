using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollisionsBetweenPlayerAndCanonEnemy : MonoBehaviour
{
    [Tooltip("First time when player's collider will touch enemy's collider")]
    public int instantDamage = 10;
    [Tooltip("When player's and enemy's collider are in contact, apply damage in certain interval")]
    public int ongoingDamage = 5;
    [Tooltip("In seconds")]
    public float damageInterval = 1f;
    private bool isTouchingPlayer = false;
    private Coroutine damageCoroutine = null;

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isTouchingPlayer = true;
            ApplyInstantDamage(collision);
            damageCoroutine = StartCoroutine(ApplyOngoingDamage(collision));
        }
    } // OnCollisionEnter2D

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            isTouchingPlayer = false;
            //print("Player And Enemy Are no longer in contact");
            if (damageCoroutine != null) {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    } // OnCollisionExit2D

    void ApplyInstantDamage(Collision2D player) {
        // Apply instant damage to the player
        player.transform.GetChild(1).GetComponent<PlayerHealthManager>().TakeDamage(instantDamage);
        //print("Applied Instant Damage : " + player.transform.GetChild(1).GetComponent<PlayerHealthManager>().healthAmount);
    } // ApplyInstantDamage

    IEnumerator ApplyOngoingDamage(Collision2D player) {
        while (isTouchingPlayer) {
            yield return new WaitForSeconds(damageInterval);
            // Apply ongoing damage to the player
            player.transform.GetChild(1).GetComponent<PlayerHealthManager>().TakeDamage(ongoingDamage);
            //print("Ongoing Damage : " + player.transform.GetChild(1).GetComponent<PlayerHealthManager>().healthAmount);
        }
    } // ApplyOngoingDamage

} // Class