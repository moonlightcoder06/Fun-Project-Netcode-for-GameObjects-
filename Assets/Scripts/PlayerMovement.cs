using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
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

    // DASH
    private float activeMoveSpeed;
    public float dashSpeed;
    public float dashLength = 0.5f, dashCoolDown = 1f;
    private float dashCounter;
    private float dashCoolCounter;

    // DASH TRAIL
    [SerializeField] private TrailRenderer trailRenderer;

    // Dash Particle
    [SerializeField] private ParticleSystem trailParticle;

    // To turn off collider during dash
    private BoxCollider2D boxCollider;

    // DASH Indicator
    // [SerializeField] private TextMeshProUGUI dashIndicatorText;
    [SerializeField] private Image dashIndicatorText;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        //DASH
        activeMoveSpeed = moveSpeed;
        trailParticle.Stop();
    }

    // Always check for inputs in Update because it runs every frame
    void Update() {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");

        //DASH
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (dashCoolCounter <= 0 && dashCounter <= 0) {
                boxCollider.enabled = false;
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                trailRenderer.emitting = true;
                trailParticle.Play();

                //Adding camera shake
                CameraShake.Instance.ShakeCamera(1f, 0.2f);

                dashIndicatorText.color = new Color(255, 254, 0, 0.5f);
            }
        }

        if (dashCounter > 0) {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0) {
                activeMoveSpeed = moveSpeed;
                trailRenderer.emitting = false;
                trailParticle.Stop();
                dashCoolCounter = dashCoolDown;
                boxCollider.enabled = true;
            }
        }

        if (dashCoolDown > 0) {
            dashCoolCounter -= Time.deltaTime;
        }

        if (dashCoolCounter <= 0 && dashCounter <= 0) {
            dashIndicatorText.color = new Color(255, 254, 0, 1f);
        }
    } // Update

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
            new Vector2(horizontalMovement * activeMoveSpeed, verticalMovement * activeMoveSpeed),
            ref movementInputSmoothVelocity,
            0.1f
            );
        // ******************** SMOOTH SPEED ENDS ********************

        rigidBody.velocity = smoothedMovementInput;
    }

} // class