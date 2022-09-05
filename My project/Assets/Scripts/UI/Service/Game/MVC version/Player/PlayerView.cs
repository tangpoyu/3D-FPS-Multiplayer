using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : AppElement, IDamageable
{
  
    [SerializeField]
    private PlayerController playerController;

    private void Start()
    {
        if (playerController.PlayerModel.Pv.IsMine)
        {
            StartCoroutine(ControllerPlayer());
            StartCoroutine(ApplyPhysics());
        }
    }

    IEnumerator ControllerPlayer()
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
        yield return new WaitForSeconds(0.020f);
        if(playerController.PlayerModel.Pv != null)
        {
            StartCoroutine(ControllerPlayer());
        }
    }

    IEnumerator ApplyPhysics()
    {
        playerController.ApplyPhysics();
        yield return new WaitForFixedUpdate();
        if (playerController.PlayerModel.Pv != null)
        {
            StartCoroutine(ApplyPhysics());
        }
    }

    public void Move()
    {
        playerController.Move(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetKey(KeyCode.LeftShift));
    }

    public void Jump()
    {
        playerController.Jump(Input.GetKeyDown(KeyCode.Space));
    }

    public void Look()
    {
        playerController.Look(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
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
