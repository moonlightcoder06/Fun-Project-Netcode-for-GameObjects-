using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldManager : MonoBehaviour
{
    // Start is called before the first frame update
    public float minDestroyTime;
    public float maxDestroyTime;

    // Update is called once per frame
    void Update()
    {
     
        if(this.gameObject.active == true) {

            StartCoroutine(DisappearCoroutine());

        }
    }

    private IEnumerator DisappearCoroutine() {

        yield return new WaitForSeconds(Random.Range(minDestroyTime, maxDestroyTime));
        this.gameObject.SetActive(false);

    }
}
