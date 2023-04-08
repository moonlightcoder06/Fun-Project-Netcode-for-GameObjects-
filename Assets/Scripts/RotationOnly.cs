using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationOnly : MonoBehaviour
{
    public float rotationSpeed = 50.0f;

    


    void Start() {
       
    }

    void Update() {
        // Rotate the GameObject continuously
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

    }


}
