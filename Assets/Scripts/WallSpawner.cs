using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSpawner : MonoBehaviour {
    public List<GameObject> wallPrefabs; // The list of wall prefabs to choose from
    public int wallCount; // The number of walls to spawn
    public float wallSize; // The size of each wall
    public float minDistance; // The minimum distance between each wall
    public float rotationSpeed; // The speed at which to rotate the prefabs

    void Start() {
        // List to keep track of the positions of spawned walls
        List<Vector2> wallPositions = new List<Vector2>();

        // Loop through and spawn the desired number of walls
        for (int i = 0; i < wallCount; i++) {
            // Get a random position within the map bounds
            Vector2 position = new Vector2(Random.Range(-55f, 55f), Random.Range(-32f, 32f));

            // Check the distance between this wall and any previously spawned walls
            bool tooClose = false;
            foreach (Vector2 prevPosition in wallPositions) {
                if (Vector2.Distance(position, prevPosition) < minDistance) {
                    tooClose = true;
                    break;
                }
            }

            // If this wall is too close to a previously spawned wall, skip it
            if (tooClose) {
                continue;
            }

            // Choose a random wall prefab from the list
            GameObject wallPrefab = wallPrefabs[Random.Range(0, wallPrefabs.Count)];

            // Spawn the wall at the random position and rotate it randomly
            GameObject wall = Instantiate(wallPrefab, position, Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));

            // Scale the wall to the desired size
            wall.transform.localScale = new Vector3(wallSize, wallSize, wallSize);

            // Add the position of this spawned wall to the list
            wallPositions.Add(position);

            // Rotate the prefab randomly and continuously
            RotatePrefab(wall);
        }
    }

    void RotatePrefab(GameObject prefab) {
        // Set the rotation speed to a random value within a certain range
        float speed = Random.Range(rotationSpeed * 0.5f, rotationSpeed * 1.5f);

        // Get the prefab's Rigidbody2D component
        Rigidbody2D rb = prefab.GetComponent<Rigidbody2D>();

        // If the prefab has a Rigidbody2D, set its angular velocity to the rotation speed
        if (rb != null) {
            rb.angularVelocity = speed;
        }
        // Otherwise, use Transform.Rotate to rotate the prefab around its local Z-axis
        else {
            StartCoroutine(RotatePrefabCoroutine(prefab, speed));
        }
    }

    IEnumerator RotatePrefabCoroutine(GameObject prefab, float speed) {
        while (true) {
            prefab.transform.Rotate(Vector3.forward * speed * Time.deltaTime);
            yield return null;
        }
    }
}
