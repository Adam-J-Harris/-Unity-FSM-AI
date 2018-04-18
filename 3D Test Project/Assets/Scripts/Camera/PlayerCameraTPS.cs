using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraTPS : MonoBehaviour {

    public float mouseSensitivity = 3.0f;
    public float clampAngle = 80.0f;

    private float cameraMoveSpeed;

    private float rotY = 0.0f; // rotation around the y axis
    private float rotX = 0.0f; // rotation around the x axis

    private float rotationSmoothTime = 0.12f;
    private Vector3 rotationSmoothVely;
    private Vector3 currentRotation;

    private GameObject cameraTarget;
    public GameObject cameraRendererFlag;

    private Vector3 cameraOffset;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 rot = transform.localRotation.eulerAngles;

        rotY = rot.y;
        rotX = rot.x;

        cameraMoveSpeed = 100;

        cameraOffset = new Vector3(0.2f, 1.2f, -0.8f);

        cameraTarget = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");

        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
        //rotY = Mathf.Clamp(rotY, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    private void LateUpdate()
    {
        CameraUpdate();
    }

    void CameraUpdate()
    {
        float movement = cameraMoveSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, cameraRendererFlag.transform.position, movement);
    }
}

