using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleCollisionBetweenPlayerAndSucidalEnemy : MonoBehaviour
{

    public int giveDamageToPlayer = 15;

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.transform.GetChild(1).GetComponent<PlayerHealthManager>().TakeDamage(giveDamageToPlayer);
            SelfDestruct();
        }
    } // OnCollisionEnter2D

    void SelfDestruct() {
        Destroy(gameObject);
    } // SelfDestruct

} // Class