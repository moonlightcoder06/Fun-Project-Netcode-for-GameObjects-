using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnning : MonoBehaviour
{
    public GameObject Enemy;
    public float spawnInterval = 10.0f;

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

        float center = 8f;

        // Generating a random position within the boundaries
        Vector3 randomPosition = new Vector3(Random.Range(screenLeft - center , screenRight + center), Random.Range(screenBottom - center, screenTop + center), 0);

        // Instantiating the GameObject at the random position
        // spawnedObject = Instantiate(Enemy, randomPosition, Quaternion.identity);
        Instantiate(Enemy, randomPosition, Quaternion.identity);
        // Attaching the Rotating script
      

    }
}

