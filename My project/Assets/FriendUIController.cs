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
    public static Action<string> OnAddFriend = delegate { };

    [SerializeField] private FriendUIModel friendUIModel;

    internal void setChatClient(ChatClient obj)
    {
        friendUIModel.ChatClient = obj;
    }

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

    private void FindPhotonFriends()
    {
        foreach (Transform child in friendUIModel.FriendItemsContainer)
        {
            Destroy(child.gameObject);
        }

        
        foreach (string name in friendUIModel.FriendList)
        {
            //Debug.Log("Create UIFriendItems");
            UIFriendItem uIFriendItem = Instantiate(friendUIModel.UIFriendItem, friendUIModel.FriendItemsContainer);
            uIFriendItem.Initialize(name);
            friendUIModel.UiFriendItems.Add(name, uIFriendItem);
        }

        if (friendUIModel.FriendList.Count != 0)
        {
            Debug.Log("add friends to friend list");
            string[] friendDisplayNames = friendUIModel.FriendList.ToArray();
            friendUIModel.ChatClient.AddFriends(friendDisplayNames);
        }

    }

   
    internal void HanleMakeFriendRequest(string obj)
    {
        UIRequestMakeFriendItem uIRequestMakeFriendItem = Instantiate(friendUIModel.UIRequestMakeFriendItem, friendUIModel.ResquestMakeFriendContanier);
        uIRequestMakeFriendItem.Initialize(obj);
        foreach(GameObject image in friendUIModel.RequestMakeFriendImage)
        {
            image.SetActive(true);
        }
    }

    internal void HandleStatusUpdated(PhotonStatus obj)
    {
        if (!friendUIModel.FriendList.Contains(obj.PlayerName)) friendUIModel.FriendList.Add(obj.PlayerName);
        if (friendUIModel.UiFriendItems.TryGetValue(obj.PlayerName, out UIFriendItem uIFriendItem))
        {
            uIFriendItem.SetStatus(obj.Status);
        }
    }



    internal void HandleAcceptMakeFriend(UIRequestMakeFriendItem uIRequestMakeFriendItem)
    {
        Destroy(uIRequestMakeFriendItem.gameObject);
    }

    internal void HandleDeclineMakeFriend(UIRequestMakeFriendItem obj)
    {
        Destroy(obj.gameObject);
    }

    internal void Exit()
    {
        friendUIModel.FriendUI.SetActive(false);
    }

    internal void OpenRequestUI()
    {
        friendUIModel.RequestUI.SetActive(true);
    }

    internal void ExitRequestUI()
    {
        friendUIModel.RequestUI.SetActive(false);
        foreach (GameObject image in friendUIModel.RequestMakeFriendImage)
        {
            image.SetActive(false);
        }
    }

    internal void AddFriend()
    {
        if (string.IsNullOrEmpty(friendUIModel.InputField.text)) return;
        OnAddFriend?.Invoke(friendUIModel.InputField.text);
    }

  
}
