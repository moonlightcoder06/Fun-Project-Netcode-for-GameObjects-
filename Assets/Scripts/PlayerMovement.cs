using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Using rigidbody we are going to move our player
    private Rigidbody2D rigidBody;
    private float verticalMovement;
    private float horizontalMovement;
    public float moveSpeed;

    // To solve diagonal speed issue i.e when you press w and a together player will move faster as compare to only pressing w,a,s,d at a time
    public float speedLimit;

    // For smooth movement
    private Vector2 smoothedMovementInput;
        // To keep track of the velocity change becuase it is required by SmoothDamp function
    private Vector2 movementInputSmoothVelocity;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Always check for inputs in Update because it runs every frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }

    // Everything regarding physics will go in FixedUpdate 
    private void FixedUpdate() {
        // ******************** DIAGONAL SPEED STARTS ********************
        // When we are moving diagonally, we are going to multiply vertical and horizontal movement by speedLimit which is going to slow it down 
        if (horizontalMovement != 0 && verticalMovement != 0) {
            horizontalMovement *= speedLimit;
            verticalMovement *= speedLimit;
        }
        // ******************** DIAGONAL SPEED ENDS ********************

        // ******************** SMOOTH SPEED STARTS ********************
        smoothedMovementInput = Vector2.SmoothDamp(smoothedMovementInput,
            new Vector2(horizontalMovement * moveSpeed, verticalMovement * moveSpeed),
            ref movementInputSmoothVelocity,
            0.1f
            );
        // ******************** SMOOTH SPEED ENDS ********************

        rigidBody.velocity = smoothedMovementInput;
    }

} // class