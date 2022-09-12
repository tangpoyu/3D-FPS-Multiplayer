using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRequestMakeFriendItem : MonoBehaviour
{
    public static Action<UIRequestMakeFriendItem> OnAcceptInvite = delegate { };
    public static Action<UIRequestMakeFriendItem> OnDeclineInvite = delegate { };

    private string senderName;
    [SerializeField] private TMP_Text senderNameText;

    public string SenderName { get => senderName; set => senderName = value; }

    public void Initialize(string senderName)
    {
        this.SenderName = senderName;
        senderNameText.SetText(senderName);
    }

    public void AcceptMakeFriend()
    {
        OnAcceptInvite?.Invoke(this);
    }

    public void DeclineMakeFriend()
    {
        OnDeclineInvite?.Invoke(this);
    }
}
