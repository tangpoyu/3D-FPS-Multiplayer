using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviourPunCallbacks
{
    [SerializeField]
    protected ItemInfo itemInfo;
    [SerializeField]
    private GameObject itemGameObject;
    protected PhotonView myPhotonView;

    public GameObject ItemGameObject { get => itemGameObject; set => itemGameObject = value; }

    protected void Awake()
    {
       myPhotonView = GetComponent<PhotonView>();
    }

    public abstract void use();
}

