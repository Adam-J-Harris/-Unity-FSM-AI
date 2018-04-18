using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Vector3 velocity;
    private Vector3 rotation;
    private Vector3 cameraRot;
    private float speed;
    private float speedSense;

    public float xCamMin;
    public float xCamMax;

    private Animator animPlayer;

    [SerializeField] private Camera PlayerCamera;

    private Rigidbody rb;

    private void Awake()
    {
        velocity = Vector3.zero;
        rotation = Vector3.zero;
        cameraRot = Vector3.zero;

        xCamMin = -90;
        xCamMax = 90;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Use this for initialization
    void Start () {

        rb = GetComponentInChildren<Rigidbody>();
        //animPlayer = GetComponentInChildren<Animator>();

        speed = 5.0f;
        speedSense = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Fire();
	}

    // Physics update call
    void FixedUpdate()
    {
        Movement();
    }
    
    // Creates a movement vector
    private void Movement()
    {
        // Keyboard movement
        float xMov = Input.GetAxisRaw("Horizontal");
        float zMov = Input.GetAxisRaw("Vertical");

        Vector3 movHorizontal = transform.right * xMov;
        Vector3 movVectical = transform.forward * zMov;

        velocity = (movHorizontal + movVectical).normalized * speed;

        //mouse movement left/right 
        float yRot = Input.GetAxisRaw("Mouse X");

        rotation = new Vector3(0f, yRot, 0f) * speedSense;

        //Camera for up/down
        //float xRot = Input.GetAxisRaw("Mouse Y");

        //float xCamRot = Mathf.Clamp(xRot, xCamMin, xCamMax);

        //cameraRot = new Vector3(xCamRot, 0f, 0f) * speedSense;

        // Move player
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

            //animPlayer.SetBool("isRunning", true);
        }
        else
        {
            //animPlayer.SetBool("isRunning", false);
        }

        //Rotate player
        if (rotation != Vector3.zero)
        {
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }

        //if (PlayerCamera != null)
        //{
        //    PlayerCamera.transform.Rotate(-cameraRot);
        //}
    }

    void Fire()
    {
        //if (Input.GetButton("Fire1") && !animPlayer.GetBool("isRunning"))
        //{
        //    animPlayer.SetBool("isFire", true);
        //}
        //else
        //{
        //    animPlayer.SetBool("isFire", false);
        //}
    }
}
