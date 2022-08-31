using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

// controller
public class PlayerController : AppElement
{
    [SerializeField]
    private PlayerModel playerModel;
    [SerializeField]
    private PhotonView pv;
    [SerializeField]
    private Camera camera;
    [SerializeField]
    private HealthBarUIController healthBarUIController; 

    internal Vector3 Move(float horizontal, float vertical, bool leftShift, Vector3 moveAmount)
    {
        Vector3 move = new Vector3(horizontal, 0, vertical).normalized;
        var smoothMoveVelocit = playerModel.SmoothMoveVelocit;
        moveAmount = Vector3.SmoothDamp(moveAmount, 
            move * (leftShift ? playerModel.SprintSpeed : playerModel.WalkSpeed),
            ref smoothMoveVelocit,
            playerModel.SmoothTime);
        return moveAmount;
    }

    internal float Jomp(bool space)
    {
       if(space && playerModel.Grounded)
        {
            return playerModel.JumpForce;
        }
        else
        {
            return 0;
        }
    }

    internal void setGounded(bool v)
    {
        playerModel.Grounded = v;
    }

    internal float Look(float mouseX, float mouseY, ref Rigidbody rb)
    {
        rb.transform.Rotate(Vector3.up * mouseX * playerModel.MouseSensitivity);
        playerModel.VerticalLookRotation += -mouseY * playerModel.MouseSensitivity;
        playerModel.VerticalLookRotation = Mathf.Clamp(playerModel.VerticalLookRotation, -70, 80);
        return playerModel.VerticalLookRotation;
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
        if (v) playerModel.Items[playerModel.CurrentitemIndex].use();
    }

    internal void SwitchWeapon(float v)
    {
        for (int i = 0; i < playerModel.Items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if ( v > 0)
        {
            if (playerModel.CurrentitemIndex >= playerModel.Items.Length - 1)
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
                EquipItem(playerModel.Items.Length - 1);
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
        playerModel.Items[playerModel.CurrentitemIndex].ItemGameObject.SetActive(true);
        if (playerModel.PreviousItemIndex != -1)
        {
           playerModel.Items[playerModel.PreviousItemIndex].ItemGameObject.SetActive(false);
        }
        playerModel.PreviousItemIndex = playerModel.CurrentitemIndex;
        if (pv.IsMine)
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
        pv.RPC(nameof(RPC_TakeDamage), pv.Owner, damage);
    }

    [PunRPC]
    public void RPC_Kill()
    {
        Hashtable hash = new Hashtable();
        hash.Add("kill", 1);
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
            pv.RPC(nameof(RPC_Kill), info.Sender);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            camera.gameObject.SetActive(true);
            PhotonNetwork.Destroy(pv);
        }
        healthBarUIController.UpdataHealth(playerModel.CurrentHealth > 0 ? playerModel.CurrentHealth : 0);
    }
}
