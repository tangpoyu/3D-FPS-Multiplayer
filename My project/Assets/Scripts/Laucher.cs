using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laucher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        print("conntect to severt");
        PhotonNetwork.ConnectUsingSettings();
    }

    // which is called when connecting to the Photon Server by Photon Server.
    public override void OnConnectedToMaster()
    {
        print("Connected to Server and then to Join the Lobby");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("joined the lobby");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
