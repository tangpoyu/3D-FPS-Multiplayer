using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using AuthenticationValues = Photon.Realtime.AuthenticationValues;


// service
public class PhotonMasterServerConnector : MonoBehaviourPunCallbacks
{
    public static Action GetPhotonFriends = delegate { };

    public static PhotonMasterServerConnector instance;
    private bool isLoad, isInLogin, isJoinedLobby, isCreatedRoom, isJoinedRoom, isCreateRoomFailed, isRoomListUpdated, isPlayerEnterRoomOrLeave
        , isInvite, isDirectJoinRoom;
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
    public bool IsInvite { get => isInvite; set => isInvite = value; }
    public bool IsDirectJoinRoom { get => isDirectJoinRoom; set => isDirectJoinRoom = value; }

    private void Awake()
    {
        PhotonChatConnector.OnChatConnected += ConnectToPhotonMasterServer;
        UIInviteItem.OnAcceptInvite += HandleRoomIviteAccept;

        PhotonNetwork.NetworkingClient.LoadBalancingPeer.DisconnectTimeout = 65535;
        if (instance == null)
        {
            instance = this;
        }
        RoomName_RoomInfo = new Dictionary<string, RoomInfo>();
        isInLogin = true;
    }

    private void OnDestroy()
    {
        PhotonChatConnector.OnChatConnected -= ConnectToPhotonMasterServer;
        UIInviteItem.OnAcceptInvite -= HandleRoomIviteAccept;
    }

    

    /////////////////// [ To connect to photon master server ] ///////////////////////////////////////////////////////////////////////////////////////////
    public void ConnectToPhotonMasterServer(ChatClient chatClient)
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
        if (IsInvite)
        {
            DirectJoinRoom(PlayerPrefs.GetString("PHOTONROOM"));
            IsInvite = false;
            return;
        }
        Debug.Log("Joined the lobby successfully.");
        GetPhotonFriends?.Invoke();
        isInLogin = false;
        isJoinedLobby = true;
        RoomName_RoomInfo.Clear();
        players = new List<Player>();
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            players.Add(player);
        }
        // IsLoad = false;
    }

    private void DirectJoinRoom(string roomName)
    {
        isDirectJoinRoom = true;
        PhotonNetwork.JoinRoom(roomName);
    }

    /////////////////// [ To create a room in photon master server ] ///////////////////////////////////////////////////////////////////////////////

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

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Created room failed.");
        isLoad = false;
        IsCreateRoomFailed = true;
        CreateRoomFailedMessage = message;
    }


    /////////////////// [ To join a room in photon master server ] ///////////////////////////////////////////////////////////////////////////////

    // ------ [ handle room invite ] -------------
    // While this method is subscribing some action,  which is called whether this component is active or not when the action is invoked.
    private void HandleRoomIviteAccept(UIInviteItem obj)
    {
        PlayerPrefs.SetString("PHOTONROOM", obj.RoomName);
        if (PhotonNetwork.InLobby) DirectJoinRoom(obj.RoomName);
        // UNDONE : Make player can accept room invite when in the room.
        else if (PhotonNetwork.InRoom)
        {
            if (PhotonNetwork.CurrentRoom.Name == obj.RoomName) return;
            LeaveRoom();
            IsInvite = true;
        }
    }

    public void JoinRoom(string roomName)
    {
        Debug.Log("To join room.");
        PhotonNetwork.JoinRoom(roomName);
        Debug.Log("set isload is true");
        IsLoad = true;
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined the Room successfully.");
        players.Clear();
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            players.Add(player);
        }
        IsLoad = false;
        isPlayerEnterRoomOrLeave = true;
        IsJoinedRoom = true;
    }

 


    /////////////////// [ Helper ] ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public bool isMasterClient()
    {
        // check player is host or not.
        return PhotonNetwork.IsMasterClient;
    }

    public string GetCurrentRoomName()
    {
        return PhotonNetwork.CurrentRoom.Name;
    }




    /////////////////// [ Start a game when in room ] //////////////////////////////////////////////////////////////////////////////////////////////////////
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
    }

    /////////////////// [ leave a room  ] //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

    /////////////////// [ if in lobby and room list updated which be called ] ///////////////////////////////////////////////////////////////////////////////
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

    /////////////////// [ Be called when player leaves or enters ] //////////////////////////////////////////////////////////////////////////////////////
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
