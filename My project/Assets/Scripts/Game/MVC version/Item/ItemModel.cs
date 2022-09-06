using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel : MonoBehaviourPunCallbacks
{
    [SerializeField]
    internal string itemName;
    [SerializeField]
    private GameObject itemGameObject;
    [SerializeField]
    internal Camera cam;
    protected PhotonView pv;

    public GameObject ItemGameObject { get => itemGameObject; set => itemGameObject = value; }

    protected void Awake()
    {
        pv = GetComponent<PhotonView>();
    }
}
