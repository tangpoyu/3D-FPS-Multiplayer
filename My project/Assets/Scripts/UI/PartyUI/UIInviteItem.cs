using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInviteItem : MonoBehaviour
{

    public static Action<UIInviteItem> OnAcceptInvite = delegate { };
    public static Action<UIInviteItem> OnDeclineInvite = delegate { };

    private string senderName, roomName;
    [SerializeField] private TMP_Text senderNameText;

    public string SenderName { get => senderName; set => senderName = value; }
    public string RoomName { get => roomName; set => roomName = value; }

    public void Initialize(string senderName, string roomName)
    {
        this.SenderName = senderName;
        this.RoomName = roomName;
        senderNameText.SetText(senderName);
    }

    public void AcceptInvite()
    {
        OnAcceptInvite?.Invoke(this);
    }

    public void DeclineInvite()
    {
        OnDeclineInvite?.Invoke(this);
    }
}
