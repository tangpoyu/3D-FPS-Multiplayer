using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayfabFriendInfo = PlayFab.ClientModels.FriendInfo;
using PhotonFriendInfo = Photon.Realtime.FriendInfo;
using Photon.Pun;
using System;
using System.Linq;

public class FriendUIView : MonoBehaviourPunCallbacks
{
    
    [SerializeField] private FriendUIController friendUIController;

    private void Awake()
    {
        PlayFabFriend.OnFriendListUpdated += HandleFriendsUpdated;
    }

    private void OnDestroy()
    {
        PlayFabFriend.OnFriendListUpdated -= HandleFriendsUpdated;
    }

    private void HandleFriendsUpdated(List<PlayfabFriendInfo> friends)
    {
        if(friends.Count != 0)
        {
            string[] friendDisplayNames = friends.Select(f => f.TitleDisplayName).ToArray();
            PhotonNetwork.FindFriends(friendDisplayNames);
        }
        else
        {
            friendUIController.UpdateFriendsList(new List<PhotonFriendInfo>());
        }
    }

    public override void OnFriendListUpdate(List<PhotonFriendInfo> friendList)
    { 
        friendUIController.UpdateFriendsList(friendList);
    }

    public void Exit()
    {
        this.gameObject.SetActive(false);
    }
}
