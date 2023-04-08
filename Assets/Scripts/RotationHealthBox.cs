using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHealthBox : MonoBehaviour
{
    public float rotationSpeed = 50.0f;
    public float scaleSpeed = 0.05f;
    


    void Start() {
       
    }

    void Update() {
        // Rotate the GameObject continuously
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

    }


}
