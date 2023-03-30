using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAndScaling: MonoBehaviour
{
    public float rotationSpeed = 50.0f;
    public float scaleSpeed = 0.05f;
    public float destroyTime = 20.0f;

    private float startTime;

    void Start() {
        startTime = Time.time;
    }

    void Update() {
        // Rotate the GameObject continuously
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        // Decrease the scale of the GameObject gradually
        float scale = Mathf.Lerp(1.0f, 0.0f, (Time.time - startTime) / destroyTime);
        transform.localScale = new Vector3(scale, scale, scale);

        // Destroy the GameObject if it has reached its destroy time
        if (Time.time - startTime > destroyTime) {
            Destroy(gameObject);
        }
    }

}
