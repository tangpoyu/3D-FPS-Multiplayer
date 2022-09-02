using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : AppElement, IDamageable
{
    private Rigidbody rb;
    private PhotonView pv;
    [SerializeField]
    private GameObject cameraHolder, healthBar, decoratorUI;
    [SerializeField]
    private PlayerController playerController;
    private Vector3 moveAmount;

    public Rigidbody Rb { get => rb; set => rb = value; }
    public PhotonView Pv { get => pv; set => pv = value; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody>();
        Pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (!pv.IsMine)
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            Destroy(cameraHolder);
            Destroy(healthBar);
            Destroy(decoratorUI);
            playerController.initialItem();
        }
        else
        {
            playerController.EquipItem(0);
        }
    }

    private void Update()
    {
        if (pv.IsMine)
        {
            Jump();
            LockAndUnlockCursor();
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Look();
            }
            SwitchWeapon();
            Attack();
            Move();
        }
    }

    private void FixedUpdate()
    {
        if(pv.IsMine) rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }

    public void Move()
    {
        moveAmount = playerController.Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetKey(KeyCode.LeftShift), moveAmount);
    }

    public void Jump()
    {
        rb.AddForce(transform.up * playerController.Jomp(Input.GetKeyDown(KeyCode.Space)));
    }

    public void Look()
    {
        var rb = Rb;
        cameraHolder.transform.localRotation = Quaternion.Euler(
            playerController.Look(Input.GetAxisRaw("Mouse X"),Input.GetAxisRaw("Mouse Y"), ref rb),
            0,
            0);
    }

    public void LockAndUnlockCursor()
    {
        playerController.LockAndUnlockCursor(Input.GetKeyDown(KeyCode.K));
    }

    public void Attack()
    {
        playerController.Attack(Input.GetMouseButtonDown(0));
    }

    public void SwitchWeapon()
    {
        playerController.SwitchWeapon(Input.GetAxisRaw("Mouse ScrollWheel"));
    }

    public void TakeDamage(float damage)
    {
        playerController.TakeDamage(damage);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        playerController.setGounded(true);
    }

    private void OnTriggerExit(Collider other)
    {
        playerController.setGounded(false);
    }

    private void OnTriggerStay(Collider other)
    {
        playerController.setGounded(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        playerController.setGounded(true);
    }

    private void OnCollisionExit(Collision collision)
    {
        playerController.setGounded(false);
    }

    private void OnCollisionStay(Collision collision)
    {
        playerController.setGounded(true);
    }
}
