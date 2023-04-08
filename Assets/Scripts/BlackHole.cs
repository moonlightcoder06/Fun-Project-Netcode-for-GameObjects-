using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{

    public GameObject blackHole;
    public float spawnInterval = 15.0f;
    public float rotationSpeed = 50.0f;

    private GameObject spawnedObject;


    void Start() {
        InvokeRepeating("SpawnGameObject", spawnInterval, spawnInterval);
    }


    void SpawnGameObject() {
        // Setting the boundaries of the screen
        float screenLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        float screenRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        float screenTop = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float screenBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;

        // Generating a random position within the boundaries
        Vector3 randomPosition = new Vector3(Random.Range(screenLeft, screenRight), Random.Range(screenBottom, screenTop), 0);

        // Instantiating the GameObject at the random position
        spawnedObject = Instantiate(blackHole, randomPosition, Quaternion.identity);

        // Attaching the Rotating script
        spawnedObject.AddComponent<RotationAndScaling>();
        spawnedObject.AddComponent<PlayerAttraction>();

    }

}
