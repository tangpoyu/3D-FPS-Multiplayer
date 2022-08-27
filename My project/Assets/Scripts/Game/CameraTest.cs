using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity, sprintSpeed, walkSpeed, smoothTime;
    private Vector3 smoothMoveVelocity, moveAmount;
    private float verticalLookRotation, horizontalRotation;
    private bool mouselocked;
    private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Look();
        }
        Move();
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void Attack()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            print(hit.normal);
            print(Quaternion.LookRotation(hit.normal));
        }
    }

    private void FixedUpdate()
    {
        this.transform.position += moveAmount;
    }

    private void Move()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            move = this.transform.forward.normalized;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            move = -this.transform.forward.normalized;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            move += transform.TransformDirection(new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0));
        }

        moveAmount = Vector3.SmoothDamp(moveAmount, move * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);
    }

    private void Look()
    {
        horizontalRotation += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        verticalLookRotation += -Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -70, 80);
        this.transform.localRotation = Quaternion.Euler(verticalLookRotation, horizontalRotation, 0);
    }

    private void LockAndUnlockCursor()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            mouselocked = !mouselocked;
        }

        Cursor.lockState = mouselocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = mouselocked ? false : true;
    }   
}
