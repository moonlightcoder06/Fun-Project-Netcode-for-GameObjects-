using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollisionBetweenPlayerAndSucidalEnemy : MonoBehaviour
{

    public int giveDamageToPlayer = 25;

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerHealthManager>().TakeDamage(giveDamageToPlayer);
            SelfDestruct();
        }
    } // OnCollisionEnter2D

    void SelfDestruct() {
        Destroy(gameObject);
    } // SelfDestruct

} // Class