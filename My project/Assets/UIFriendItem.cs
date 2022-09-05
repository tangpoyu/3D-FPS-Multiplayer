using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFriendItem : MonoBehaviour
{
    [SerializeField] private TMP_Text friendNameText;
    [SerializeField] private FriendInfo friend;

    public static Action<string> OnRemoveFriend = delegate { };

    public void Initialize(FriendInfo friend)
    {
        this.friend = friend;
        friendNameText.SetText(friend.UserId);
    }

    public void RemoveFriend()
    {
        OnRemoveFriend?.Invoke(friend.UserId);
    }
}
