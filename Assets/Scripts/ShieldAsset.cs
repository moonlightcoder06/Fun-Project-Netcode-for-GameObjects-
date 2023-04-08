using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldAsset : MonoBehaviour
{
    public float minDestroyTime;
    public float maxDestroyTime;

    void Start() {

        StartCoroutine(DisappearCoroutine());
    }


    void OnTriggerEnter2D(Collider2D collision) {


        if (collision.gameObject.CompareTag("Player")) {

            collision.transform.GetChild(2).gameObject.SetActive(true);
            Destroy(this.gameObject);

        } 
        

    } 

    //private void OnTriggerExit2D(Collider2D collision) {

    //    if (collision.gameObject.CompareTag("Player")) {

            

    //    }
    //}


    private IEnumerator DisappearCoroutine() {

        yield return new WaitForSeconds(Random.Range(minDestroyTime, maxDestroyTime));
        Destroy(this.gameObject);

    }

}
