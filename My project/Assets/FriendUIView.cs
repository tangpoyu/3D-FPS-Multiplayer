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
        PhotonChatConnector.OnMakeFriendRequest += HanleMakeFriendRequest;
        UIRequestMakeFriendItem.OnAcceptInvite += HandleAcceptMakeFriend;
        UIRequestMakeFriendItem.OnDeclineInvite += HandleDeclineMakeFriend;
    }
    private void OnDestroy()
    {
        PlayFabFriend.OnFriendListUpdated -= HandleFriendsUpdated;
        PhotonChatConnector.OnChatConnected -= HandleChatConnected;
        PhotonChatConnector.OnStatusUpdated -= HandleStatusUpdated;
        PhotonChatConnector.OnMakeFriendRequest -= HanleMakeFriendRequest;
        UIRequestMakeFriendItem.OnAcceptInvite -= HandleAcceptMakeFriend;
        UIRequestMakeFriendItem.OnDeclineInvite -= HandleDeclineMakeFriend;
    }

    private void HandleChatConnected(ChatClient obj)
    {
        friendUIController.setChatClient(obj);
    }

    private void HandleFriendsUpdated(List<PlayfabFriendInfo> friends)
    {
        friendUIController.UpdateFriendsList(friends);
    }

    private void HandleStatusUpdated(PhotonStatus obj)
    {
        friendUIController.HandleStatusUpdated(obj);

    }

    private void HanleMakeFriendRequest(string obj)
    {
        friendUIController.HanleMakeFriendRequest(obj);
    }

    private void HandleAcceptMakeFriend(UIRequestMakeFriendItem uIRequestMakeFriendItem)
    {
        friendUIController.HandleAcceptMakeFriend(uIRequestMakeFriendItem);
    }

    private void HandleDeclineMakeFriend(UIRequestMakeFriendItem obj)
    {
        friendUIController.HandleDeclineMakeFriend(obj);
    }
    public void Exit()
    {
        friendUIController.Exit();
    }

    public void OpenRequestUI()
    {
        friendUIController.OpenRequestUI();
    }

    public void ExitRequestUI()
    {
        friendUIController.ExitRequestUI();
    }

    public void AddFriend()
    {
        friendUIController.AddFriend();
    }

 
}
