using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using Unity.VisualScripting;

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

    // Getting Post Process Global
    [SerializeField] private PostProcessVolume postProcess;
    private LensDistortion LensDist;
    private ChromaticAberration ChromeAb;

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
        // For Trail Particle
        trailParticle.Stop();

        // Getting LensDistortion
        postProcess.profile.TryGetSettings(out  LensDist);
        postProcess.profile.TryGetSettings(out ChromeAb);

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

                // For Trail Particle
                trailParticle.Play();

                // Adding LensDist
                LensDist.intensity.value = Mathf.Lerp(-10.0f, -50.0f, Time.deltaTime * 300);

                ChromeAb.intensity.value = Mathf.Lerp(0.35f, 3.0f, Time.deltaTime * 300);

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

                // For Trail Particle
                trailParticle.Stop();

                // Adding LensDist
                LensDist.intensity.value = -10.0f;

                ChromeAb.intensity.value = 0.35f;

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