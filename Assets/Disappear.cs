using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disappear : MonoBehaviour
{
    public float disappearTime = 5.0f; // the time it takes for the prefab to disappear in seconds
   
    private void Start() {
        StartCoroutine(DisappearCoroutine());
    }

    private IEnumerator DisappearCoroutine() {

            yield return new WaitForSeconds(disappearTime);
            Destroy(gameObject);
        
    }
}

