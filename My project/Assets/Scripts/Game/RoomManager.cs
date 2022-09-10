using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

// View
public class RoomManager : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        GameObject playerManager = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        playerManager.name = "myPlayerManager";
        playerManager.transform.SetParent(this.transform);
    }

    //public override void OnEnable()
    //{
    //    base.OnEnable();
    //    SceneManager.sceneLoaded += OnSceneLoaded;
    //}

    //public override void OnDisable()
    //{
    //    base.OnDisable();
    //}
   
    //private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    //{
    //    if(arg0.name == "Game")
    //    {
    //        GameObject playerManager = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
    //        playerManager.name = "myPlayerManager";
    //        playerManager.transform.SetParent(this.transform);
    //    }
    //}
}
