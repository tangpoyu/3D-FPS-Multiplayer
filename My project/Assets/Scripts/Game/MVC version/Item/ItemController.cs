using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemController : MonoBehaviour
{
    protected PhotonView pv;

    protected void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    public abstract void use();
}
