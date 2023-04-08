using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthIncrease : MonoBehaviour
{

    public float HealAmount = 1;
    private GameObject playerHealthManager;
    private bool collisionStarted;
    public float minDestroyTime;
    public float maxDestroyTime;

    void Start() {

        playerHealthManager = GameObject.FindWithTag("HealthManager");

        StartCoroutine(DisappearCoroutine());
    }


    void OnTriggerStay2D(Collider2D collision) {


        if (collision.gameObject.CompareTag("Player")) {

            playerHealthManager.GetComponent<PlayerHealthManager>().Heal(HealAmount);

            //HealAmount = HealAmount + 0.001f;

        } 
        

    } // OnCollisionEnter2D

    private IEnumerator DisappearCoroutine() {

        yield return new WaitForSeconds(Random.Range(minDestroyTime, maxDestroyTime));
        Destroy(transform.parent.gameObject);

    }

}
