using Photon.Chat;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FriendUIController : MonoBehaviour
{
    public static Action<PhotonStatus> OnStatusUpdated = delegate { };

    [SerializeField] private FriendUIModel friendUIModel;
   
    internal void UpdateFriendsList(List<PlayFab.ClientModels.FriendInfo> friends)
    {
        if (friendUIModel.FriendList.Count > 0)
        {
            string[] friendDisplayNames = friendUIModel.FriendList.ToArray();
            friendUIModel.ChatClient.RemoveFriends(friendDisplayNames);
            friendUIModel.UiFriendItems.Clear();
        }
       
        friendUIModel.FriendList = friends.Select(f => f.TitleDisplayName).ToList();
        FindPhotonFriends();
    }

    internal void setChatClient(ChatClient obj)
    {
        friendUIModel.ChatClient = obj;
    }

    private void FindPhotonFriends()
    {
        if (friendUIModel.FriendList.Count != 0)
        {
            string[] friendDisplayNames = friendUIModel.FriendList.ToArray();
            friendUIModel.ChatClient.AddFriends(friendDisplayNames);
        }

        foreach (Transform child in friendUIModel.FriendItemsContainer)
        {
            Destroy(child.gameObject);
        }

        //Debug.Log("Create UIFriendItems");
        foreach (string name in friendUIModel.FriendList)
        {
            UIFriendItem uIFriendItem = Instantiate(friendUIModel.UIFriendItem, friendUIModel.FriendItemsContainer);
            uIFriendItem.Initialize(name);
            friendUIModel.UiFriendItems.Add(name, uIFriendItem);
        }
    }

    internal void HandleStatusUpdated(PhotonStatus obj)
    {
        if (friendUIModel.UiFriendItems.TryGetValue(obj.PlayerName, out UIFriendItem uIFriendItem))
        {
            uIFriendItem.SetStatus(obj.Status);
        }
    }

    internal void Exit()
    {
        friendUIModel.FriendUI.SetActive(false);
    }
}
