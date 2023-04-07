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

    void OnCollisionEnter2D(Collision2D collision) {

        print(collision.gameObject.name);

        if (collision.gameObject.CompareTag("Wall")) {
            SelfDestruct();
            print("Wall");
        }

        if (collision.gameObject.CompareTag("Player")) {
            playerHealthManager.GetComponent<PlayerHealthManager>().TakeDamage(giveDamageToPlayer);
            SelfDestruct();
        }
        
       

        
    } // OnCollisionEnter2D

    void SelfDestruct() {
        Destroy(gameObject);
    } // SelfDestruct

} // Class