using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFriendItem : MonoBehaviour
{
    public static Action<string> OnRemoveFriend = delegate { };
    public static Action<string> OnInviteFriend = delegate { };

    [SerializeField] private TMP_Text friendNameText;
    [SerializeField] private Button inviteButton;
    [SerializeField] private Image statusImage;
    [SerializeField] private Color onlineImage;
    [SerializeField] private Color offlineImage;

    public void SetStatus(int status)
    {
        if(status == ChatUserStatus.Online)
        {
            Debug.Log(friendNameText.text + " sets online color");
            inviteButton.interactable = true;
            statusImage.color = onlineImage;
        }
        else
        {
            Debug.Log(friendNameText.text + " sets offline color");
            inviteButton.interactable = false;
            statusImage.color = offlineImage;
        }
    }

    private void Update()
    {
        //if (PhotonNetwork.InRoom)
        //{
        //    inviteButton.interactable = true;
        //}
        //else
        //{
        //    inviteButton.interactable = false;
        //}
    }

    public void Initialize(string friendName)
    {
        friendNameText.SetText(friendName);
    }

    public void RemoveFriend()
    {
        OnRemoveFriend?.Invoke(friendNameText.text);
    }

    public void InviteFriend()
    {
        OnInviteFriend?.Invoke(friendNameText.text);
    }
}
