using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendUIController : MonoBehaviour
{
    [SerializeField] private FriendUIModel friendUIModel;


    internal void UpdateFriendsList(List<FriendInfo> friendList)
    {
        foreach(Transform child in friendUIModel.FriendItemsContainer)
        {
            Destroy(child.gameObject);
        }

        foreach(FriendInfo friendInfo in friendList)
        {
            UIFriendItem uIFriendItem = Instantiate(friendUIModel.UIFriendItem, friendUIModel.FriendItemsContainer);
            uIFriendItem.Initialize(friendInfo);
        }
    }
}
