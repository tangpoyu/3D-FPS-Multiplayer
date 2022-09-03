using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

// controller
public class PlayerController : AppElement
{
    [SerializeField]
    private PlayerModel playerModel;
   
    public PlayerModel PlayerModel { get => playerModel; set => playerModel = value; }

    internal void Move(float horizontal, float vertical, bool leftShift)
    {
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized;
        var smoothMoveVelocit = playerModel.SmoothMoveVelocit;
        playerModel.MoveAmount = Vector3.SmoothDamp(playerModel.MoveAmount, 
            move * (leftShift ? playerModel.SprintSpeed : playerModel.WalkSpeed),
            ref smoothMoveVelocit,
            playerModel.SmoothTime);
    }

    internal void ApplyPhysics()
    {
        playerModel.Rb.MovePosition(playerModel.Rb.position + transform.TransformDirection(playerModel.MoveAmount) * Time.fixedDeltaTime);
    }

    internal void Jump(bool space)
    {
       if(space && playerModel.Grounded)
        {
            playerModel.Rb.AddForce(transform.up * playerModel.JumpForce);
        }
    }

    internal void setGounded(bool v)
    {
        playerModel.Grounded = v;
    }

    internal void Look(float mouseX, float mouseY)
    {
        playerModel.Rb.transform.Rotate(Vector3.up * mouseX * playerModel.MouseSensitivity);
        playerModel.VerticalLookRotation += -mouseY * playerModel.MouseSensitivity;
        playerModel.VerticalLookRotation = Mathf.Clamp(playerModel.VerticalLookRotation, -70, 80);
        playerModel.CameraHolder.transform.localRotation = Quaternion.Euler(playerModel.VerticalLookRotation, 0, 0);
    }

    internal void LockAndUnlockCursor(bool v)
    {
        if (v)
        {
            playerModel.Mouselocked = !playerModel.Mouselocked;
        }
        Cursor.lockState = playerModel.Mouselocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = playerModel.Mouselocked ? false : true;
    }

    public void UIOpenOrClose(bool isOpen)
    {
        playerModel.Mouselocked = (isOpen) ? false : true;
        playerModel.CanShoot = (isOpen) ? false : true;
        Cursor.lockState = playerModel.Mouselocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = playerModel.Mouselocked ? false : true;
    }

    internal void Attack(bool v)
    {
        if (!playerModel.CanShoot) return;
        if (v) playerModel.SingleShotGunController[playerModel.CurrentitemIndex].use();
    }

    internal void SwitchWeapon(float v)
    {
        for (int i = 0; i < playerModel.SingleShotGunController.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if ( v > 0)
        {
            if (playerModel.CurrentitemIndex >= playerModel.SingleShotGunController.Length - 1)
            {
                EquipItem(0);
            }
            else
            {
                EquipItem(playerModel.CurrentitemIndex + 1);
            }
        } else if( v < 0)
        {
            if (playerModel.CurrentitemIndex <= 0)
            {
                EquipItem(playerModel.SingleShotGunController.Length - 1);
            }
            else
            {
                EquipItem(playerModel.CurrentitemIndex - 1);
            }
        }
    }


    public void EquipItem(int index)
    {
        if (index == playerModel.PreviousItemIndex) return;
        playerModel.CurrentitemIndex = index;
        playerModel.SingleShotGunController[playerModel.CurrentitemIndex].gunModel.ItemGameObject.SetActive(true);
        if (playerModel.PreviousItemIndex != -1)
        {
            playerModel.SingleShotGunController[playerModel.PreviousItemIndex].gunModel.ItemGameObject.SetActive(false);
        }
        playerModel.PreviousItemIndex = playerModel.CurrentitemIndex;
        if (playerModel.Pv.IsMine)
        {
            // LEARN: 
            Hashtable hash = new Hashtable();
            hash.Add("currentitemIndex", playerModel.CurrentitemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    public void initialItem()
    {
        EquipItem(playerModel.CurrentitemIndex);
    }

    internal void TakeDamage(float damage)
    {
        playerModel.Pv.RPC(nameof(RPC_TakeDamage), playerModel.Pv.Owner, damage);
    }

    [PunRPC]
    public void RPC_Kill(Player killer)
    {
        PlayerModel killerData = FindObjectsOfType<PlayerModel>().SingleOrDefault(x => x.Pv.Owner == killer);
        killerData.Kill++;
        Hashtable hash = new Hashtable();
        hash.Add("kill", killerData.Kill);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    [PunRPC]
    public void RPC_TakeDamage(float damage, PhotonMessageInfo info)
    {
        playerModel.CurrentHealth -= damage;
        if (playerModel.CurrentHealth <= 0)
        {
            playerModel.Death++;
            Hashtable hash = new Hashtable();
            hash.Add("death", playerModel.Death);
            playerModel.Pv.RPC(nameof(RPC_Kill), info.Sender, info.Sender);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            playerModel.GhostModeCamera.gameObject.SetActive(true);
            GetComponentInParent<PlayerManager>().PlayerRespawn();
            PhotonNetwork.Destroy(playerModel.Pv);
        }
        playerModel.HealthBarUIController.UpdataHealth(playerModel.CurrentHealth > 0 ? playerModel.CurrentHealth : 0);
    }
}
