using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    #region Variables

    // ******************** ROTATION STARTS ********************
    private Camera mainCam;
    private Vector3 currentMousePosition;
    // ******************** ROTATION ENDS ********************


    // ******************** BULLET FIRING STARTS ********************
    public GameObject bullet;
    // bulletTransform this is the position from where the bullet will spawn i.e black square
    public Transform bulletTransform;
    public bool canFire;
    // timer is use to track how frequently the player can fire bullets
    private float timer;
    public float timeBetweenFiring;
    // ******************** BULLET FIRING ENDS ********************

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        // ******************** ROTATION STARTS ********************
        // Here we are setting this vector 3 to Input.mousePosition i.e where every we are moving the mouse.
        currentMousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = currentMousePosition - transform.position;

        // rotZ because we are rotating on the z axis
        // Atan2 will give angles in radians
        // Rad2Deg will convert our to rotation to degree
        // So this is how we are creating rotation
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        // Finally we are going to set the rotation of our transform to Quaternion.Euler() which is a rotation function
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        // ******************** ROTATION ENDS ********************



        // ******************** BULLET FIRING STARTS ********************
        // So, if the player shoot first time canFire will become false and player can't shoot no more. So, we are setting it back to true.
        if (!canFire) {
            // So this means timer will go up or increase with time of the game i.e as we play the game
            timer += Time.deltaTime;
            // Eg : If we set timeBetweenFiring as 5 seconds then play can shoot bullet in every 5 seconds
            if (timer > timeBetweenFiring) {
                canFire = true;
                // Because timer will go up and up and this condition will always be true, thats why we are setting it back to 0
                timer = 0;
            }
        }

        // To make sure that player can't shoot frequently
        if (Input.GetMouseButton(0) && canFire) {
            canFire = false;
            Instantiate(bullet, bulletTransform.position, Quaternion.identity);
        }
        // ******************** BULLET FIRING ENDS ********************
    } // Update

} // Class