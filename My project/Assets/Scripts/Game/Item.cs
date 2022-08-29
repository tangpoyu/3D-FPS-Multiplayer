using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    protected ItemInfo itemInfo;
    [SerializeField]
    private GameObject itemGameObject;
    protected PhotonView photonView;

    public GameObject ItemGameObject { get => itemGameObject; set => itemGameObject = value; }

    protected void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public abstract void use();
}
