using Photon.Pun;
using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayFabFriend : MonoBehaviour
{
    public static Action<List<FriendInfo>> OnFriendListUpdated = delegate { };
    private List<FriendInfo> friends;

    private void Awake()
    {
        friends = new List<FriendInfo>();
        PhotonMasterServerConnector.GetPhotonFriends += GetPlayfabFriends;
        AddFriend.OnAddFriend += HandleAddPlayfabFriend;
        UIFriendItem.OnRemoveFriend += HandleRemoveFriend;
    }

    private void OnDestroy()
    {
        PhotonMasterServerConnector.GetPhotonFriends -= GetPlayfabFriends;
        AddFriend.OnAddFriend -= HandleAddPlayfabFriend;
        UIFriendItem.OnRemoveFriend -= HandleRemoveFriend;
    }

  
    /////////////////////////////////////// handle add friend ////////////////////////////////////////
    private void HandleAddPlayfabFriend(string name)
    {
        var request = new AddFriendRequest { FriendTitleDisplayName = name };
        PlayFabClientAPI.AddFriend(request, OnFriendAddedSuccess, OnFailure);

    }

    private void OnFriendAddedSuccess(AddFriendResult obj)
    {
        GetPlayfabFriends();
    }

    private void GetPlayfabFriends()
    {
        var request = new GetFriendsListRequest { IncludeSteamFriends = false, IncludeFacebookFriends = false, XboxToken = null };
        PlayFabClientAPI.GetFriendsList(request, OnFriendsListSuccess, OnFailure);
    }

    private void OnFriendsListSuccess(GetFriendsListResult obj)
    {
        friends = obj.Friends;
        // all method which subscribe this action will receive this parameter [ obj.Friends ( List< PlayfabFriendInfo > ) ]
        OnFriendListUpdated?.Invoke(obj.Friends);
    }


    //////////////////////////////////////// handle remove friend ////////////////////////////////////////
    private void HandleRemoveFriend(string obj)
    {
        string id = friends.FirstOrDefault(f => f.TitleDisplayName == obj).FriendPlayFabId;
        var request = new RemoveFriendRequest { FriendPlayFabId = id };
        PlayFabClientAPI.RemoveFriend(request, OnFriendRemoveSuccess, OnFailure);
    }

    private void OnFriendRemoveSuccess(RemoveFriendResult obj)
    {
        GetPlayfabFriends();
    }


    //////////////////////////////////////// Display error message ///////////////////////////////////////
    private void OnFailure(PlayFabError obj)
    {
        Debug.Log($"PlayFab Friends system Error occured  {obj.GenerateErrorReport()}");
    }
}
