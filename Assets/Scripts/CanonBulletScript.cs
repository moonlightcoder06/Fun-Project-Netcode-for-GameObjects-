using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBulletScript : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    public float bulletSpeed = 10f;
    public float bulletDamageAmountToPlayer = 5f;

    private GameObject playerHealthManager;

    private void Start() {
        playerHealthManager = GameObject.FindWithTag("HealthManager");
    }

    public void ApplyForceOnBullet(Vector3 direction) {

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = direction.normalized * bulletSpeed;

        // If bullet deosn't hit any wall or ground then self destruct after 2 seconds
        Invoke("SelfDestruct", 2);

    } // ApplyForceOnBullet

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 6) // When bullet hits ground i.e "Wall" then destroy itself.
        {
            Destroy(gameObject);
        } else if (collision.CompareTag("Player")) {
            //collision.transform.GetChild(1).GetComponent<PlayerHealthManager>().TakeDamage(bulletDamageAmountToPlayer);
            playerHealthManager.GetComponent<PlayerHealthManager>().TakeDamage(bulletDamageAmountToPlayer);
            SelfDestruct();
        }
    }

    void SelfDestruct() {
        Destroy(gameObject);
    }

} // Class