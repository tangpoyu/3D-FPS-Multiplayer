using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// service
public class Laucher : MonoBehaviourPunCallbacks
{
  

    public static Laucher instance;
    private bool isLoad, isInLogin, isJoinedLobby, isCreatedRoom, isJoinedRoom, isCreateRoomFailed, isRoomListUpdated, isPlayerEnterRoomOrLeave;
    private string createRoomFailedMessage;
    
    private Dictionary<string, RoomInfo> roomName_RoomInfo;
    private List<Player> players;

    public bool IsLoad { get => isLoad; set => isLoad = value; }
    public bool IsInLogin { get => isInLogin; set => isInLogin = value; }
    public bool IsJoinedLobby { get => isJoinedLobby; set => isJoinedLobby = value; }
    public bool IsCreatedRoom { get => isCreatedRoom; set => isCreatedRoom = value; }
    public bool IsJoinedRoom { get => isJoinedRoom; set => isJoinedRoom = value; }
    public bool IsCreateRoomFailed { get => isCreateRoomFailed; set => isCreateRoomFailed = value; }
    public string CreateRoomFailedMessage { get => createRoomFailedMessage; set => createRoomFailedMessage = value; }
    public bool IsRoomListUpdated { get => isRoomListUpdated; set => isRoomListUpdated = value; }
    public Dictionary<string, RoomInfo> RoomName_RoomInfo { get => roomName_RoomInfo; set => roomName_RoomInfo = value; }
    public List<Player> Players { get => players; set => players = value; }
    public bool IsPlayerEnterRoomOrLeave { get => isPlayerEnterRoomOrLeave; set => isPlayerEnterRoomOrLeave = value; }
 

    private void Awake()
    {
        PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 65535;
        if (instance == null)
        {
            instance = this;
        }
        RoomName_RoomInfo = new Dictionary<string, RoomInfo>();
        isInLogin = true;
    }

    public void conntect()
    {
        string nickname = PlayerPrefs.GetString("USERNAME");
        Debug.Log($"To conntect to Phton server as {nickname} player.");
        PhotonNetwork.AuthValues = new AuthenticationValues(nickname);
        // Make all player can enter the Game Scene at the same time.
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = nickname;
        PhotonNetwork.ConnectUsingSettings();
        // IsLoad = true;
    }

    // which is called when connecting to the Photon Server by Photon Server.
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Server and then to Join the Lobby.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined the lobby successfully.");
        isInLogin = false;
        isJoinedLobby = true;
        // all method which subscribe this action will be executed with no parameter.
 
        RoomName_RoomInfo.Clear();
        players = new List<Player>();
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            players.Add(player);
        }
        // IsLoad = false;
    }

    public void CreateRoom(string roomName)
    {

        Debug.Log("To creat a room.");
        if (string.IsNullOrEmpty(roomName)) return;
        PhotonNetwork.CreateRoom(roomName);
        IsLoad = true;
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created a room successfully.");
        isPlayerEnterRoomOrLeave = true;
        players.Clear();
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            players.Add(player);
        }
        isLoad = false;
        IsCreatedRoom = true;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined the Room successfully.");
        isPlayerEnterRoomOrLeave = true;
        players.Clear();
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            players.Add(player);
        }
        IsLoad = false;
        IsJoinedRoom = true;
    }

    public bool isMasterClient()
    {
        // check player is host or not.
        return PhotonNetwork.IsMasterClient;
    }

    public string GetCurrentRoomName()
    {
        return PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Created room failed.");
        isLoad = false;
        IsCreateRoomFailed = true;
        CreateRoomFailedMessage = message;
    }

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    public void LeaveRoom()
    {
      
        PhotonNetwork.LeaveRoom();
        isCreatedRoom = false;
        isJoinedRoom = false;
        isLoad = true;
    }

    public override void OnLeftRoom()
    {
        isLoad = false;
        isJoinedLobby = true;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("room list update");
        isRoomListUpdated = true;
        foreach(RoomInfo room in roomList)
        {
            // Need to check if room is removed from the List or not then initiate, becase
            // Photon doesn't remove the RoomInfo when nobody in Room instead setting the
            // RemovedFromList of field of RoomInfo to true.
            if (!room.RemovedFromList)
            {
                RoomName_RoomInfo[room.Name] = room;
            }
            else
            {
                RoomName_RoomInfo.Remove(room.Name);

            }
        }                                      
        
    }

    public void JoinRoom(string roomName)
    {
        Debug.Log("To join room.");
        PhotonNetwork.JoinRoom(roomName);
        isLoad = true;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"{newPlayer} Joined room successfully.");
        isPlayerEnterRoomOrLeave = true;
        players.Add(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        isPlayerEnterRoomOrLeave = true;
        players.Remove(otherPlayer);
    }
}
