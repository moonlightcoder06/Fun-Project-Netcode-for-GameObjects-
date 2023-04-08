using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoxSpawner : MonoBehaviour
{

    public GameObject HealthBox;
    public float spawnInterval = 15.0f;
    public float rotationSpeed = 50.0f;



    private GameObject spawnedObject;


    void Start() {
        InvokeRepeating("SpawnGameObject", spawnInterval, spawnInterval);
    }

    
    void SpawnGameObject() {


        // Generating a random position within the boundaries
        //Vector3 randomPosition = new Vector3(Random.Range(screenLeft, screenRight), Random.Range(screenBottom, screenTop), 0);
      
        Vector2 randomPosition = new Vector2(Random.Range(-55f, 55f), Random.Range(-32f, 32f));
        // Instantiating the GameObject at the random position
        spawnedObject = Instantiate(HealthBox, randomPosition, Quaternion.identity);

        // Attaching the Rotating script
        spawnedObject.AddComponent<RotationHealthBox>();

    }


}
