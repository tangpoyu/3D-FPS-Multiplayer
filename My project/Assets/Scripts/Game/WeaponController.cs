using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class WeaponController : MonoBehaviourPunCallbacks
{
    private PhotonView pv;
    [SerializeField]
    Item[] items;
    private int itemIndex, previousItemIndex = -1;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if(pv.IsMine)
        EquipItem(0);
    }

    private void Update()
    {
        if (!pv.IsMine) return;
        SwitchWeapon();
        Attack();
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            items[itemIndex].use();
        }
    }

    public void SwitchWeapon()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()))
            {
                EquipItem(i);
                break;
            }
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
        {
            if (itemIndex >= items.Length - 1)
            {
                EquipItem(0);
            }
            else EquipItem(itemIndex + 1);
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            if (itemIndex <= 0)
            {
                EquipItem(items.Length - 1);
            }
            else
            {
                EquipItem(itemIndex - 1);
            }
        }
    }

    public void EquipItem(int index)
    {
        if (index == previousItemIndex) return;
        itemIndex = index;
        items[itemIndex].ItemGameObject.SetActive(true);
        if (previousItemIndex != -1)
        {
            items[previousItemIndex].ItemGameObject.SetActive(false);
        }
        previousItemIndex = itemIndex;

        if (pv.IsMine)
        {
            // LEARN: 
            Hashtable hash = new Hashtable();
            hash.Add("itemIndex", itemIndex);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!pv.IsMine && targetPlayer == pv.Owner &&¡@changedProps.ContainsKey("itemIndex"))
        {
            EquipItem((int)changedProps["itemIndex"]);
        }
    }
}
