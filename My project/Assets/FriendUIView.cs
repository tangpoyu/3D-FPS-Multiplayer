using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayfabFriendInfo = PlayFab.ClientModels.FriendInfo;
using PhotonFriendInfo = Photon.Realtime.FriendInfo;
using Photon.Pun;
using System;
using System.Linq;
using Photon.Chat;

public class FriendUIView : MonoBehaviour
{
    
    [SerializeField] private FriendUIController friendUIController;

    private void Awake()
    {
        PlayFabFriend.OnFriendListUpdated += HandleFriendsUpdated;
        PhotonChatConnector.OnChatConnected += HandleChatConnected;
        PhotonChatConnector.OnStatusUpdated += HandleStatusUpdated;     
    }

    private void OnDestroy()
    {
        PlayFabFriend.OnFriendListUpdated -= HandleFriendsUpdated;
        PhotonChatConnector.OnChatConnected -= HandleChatConnected;
        PhotonChatConnector.OnStatusUpdated -= HandleStatusUpdated;
      
    }

    private void HandleFriendsUpdated(List<PlayfabFriendInfo> friends)
    {
        friendUIController.UpdateFriendsList(friends);
    }



    private void HandleChatConnected(ChatClient obj)
    {
        friendUIController.setChatClient(obj);
    }

    private void HandleStatusUpdated(PhotonStatus obj)
    {
        friendUIController.HandleStatusUpdated(obj);
    }

    public void Exit()
    {
        friendUIController.Exit();
    }
}
