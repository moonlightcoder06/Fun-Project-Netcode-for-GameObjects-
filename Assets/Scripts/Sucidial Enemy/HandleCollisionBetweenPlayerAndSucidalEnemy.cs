using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandleCollisionBetweenPlayerAndSucidalEnemy : MonoBehaviour
{

    public int giveDamageToPlayer = 25;
    private GameObject playerHealthManager;

    void Start() {

        playerHealthManager = GameObject.FindWithTag("HealthManager");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        // if ( (collision.gameObject.CompareTag("Wall")) || (collision.gameObject.CompareTag("Shield")) )  {
        if ((collision.gameObject.CompareTag("Wall")) || (collision.gameObject.CompareTag("Shield"))) {
            print("Shield");
            SelfDestruct();
        } 
        
        if (collision.gameObject.CompareTag("Player")) {
            print("Player");
            playerHealthManager.GetComponent<PlayerHealthManager>().TakeDamage(giveDamageToPlayer);
            SelfDestruct();
        }

    }


    void SelfDestruct() {
        Destroy(gameObject);
    } // SelfDestruct

} // Class