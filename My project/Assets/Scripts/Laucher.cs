using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// service
public class Laucher : MonoBehaviourPunCallbacks
{
    public static Laucher instance;
    private bool isLoad,isJoinedLobby, isJoinedRoom, isCreateRoomFailed;
    private string createRoomFailedMessage;

    public bool IsLoad { get => isLoad; set => isLoad = value; }
    public bool IsJoinedLobby { get => isJoinedLobby; set => isJoinedLobby = value; }
    public bool IsJoinedRoom { get => isJoinedRoom; set => isJoinedRoom = value; }
    public bool IsCreateRoomFailed { get => isCreateRoomFailed; set => isCreateRoomFailed = value; }
    public string CreateRoomFailedMessage { get => createRoomFailedMessage; set => createRoomFailedMessage = value; }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        print("conntect to server");
        PhotonNetwork.ConnectUsingSettings();
        IsLoad = true;
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
        IsLoad = false;
        IsJoinedLobby = true;
    }

    public void CreateRoom(string roomName)
    {
        print("creat room");
        if (string.IsNullOrEmpty(roomName)) return;
        PhotonNetwork.CreateRoom(roomName);
        IsLoad = true;
    }

    public override void OnJoinedRoom()
    {
        print("Joined the Room");
        IsLoad = false;
        IsJoinedRoom = true;
    }

    public string GetCurrentRoomName()
    {
        return PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("creat room failed");
        isLoad = false;
        IsCreateRoomFailed = true;
        CreateRoomFailedMessage = message;
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        isJoinedRoom = false;
        isLoad = true;
    }
    

}
