using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Here everything will go in start function as we want everything to run in start and not run again

    private Vector3 currentMousePos;
    private Camera mainCam;
    private Rigidbody2D rigidBody;
    // force is for the speed
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rigidBody = GetComponent<Rigidbody2D>();
        currentMousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        // Here we are making a direction vector3 to give the bullet a direction to go in
        // This is going to give us the position of the mouse in a direction so the bullet can go towards that
        Vector3 direction = currentMousePos - transform.position;
        // normalized means we give it a magnitude of 1 i.e regardless whether the mouse cursor is further away from the player or closer to the player the bullet will not change its speed 
        rigidBody.velocity = new Vector2(direction.x, direction.y).normalized * force;


        // ********************** BULLET ROTATION **********************
        // This will rotate the bullet towards the cursor
        // For whatever the reason this gives us the right rotation that we need
        Vector3 rotation = transform.position - currentMousePos;
        float rotz = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotz);

        // If bullet deosn't hit any wall or ground then self destruct after 2 seconds
        Invoke("SelfDestruct", 2);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 6) // When bullet hits ground i.e "Wall" then destroy itself.
        {
            Destroy(gameObject);
        }
    }

    void SelfDestruct() {
        Destroy(gameObject);
    }

} // Class