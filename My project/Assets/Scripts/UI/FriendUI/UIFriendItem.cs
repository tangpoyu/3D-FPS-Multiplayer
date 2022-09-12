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
    private bool isOnline;

    // which is called only once when the fist time which is from inactive to active.
    private void Awake()
    {
        
    }

    // which is called when which is destroyed not inactive.
    private void OnDestroy()
    {
        
    }

    // which is called when this monobehavior ( component ) is active in hirerarchy. ( inactive not )
    private void Update()
    {
        if (PhotonNetwork.InRoom && isOnline)
        {
            inviteButton.interactable = true;
        }
        else
        {
            inviteButton.interactable = false;
        }
    }

    public void SetStatus(int status)
    {

        // UNDONE : change color to playingImage when status is playing.
        if(status == ChatUserStatus.Online)
        {
            Debug.Log(friendNameText.text + " sets online color");
            isOnline = true;
            statusImage.color = onlineImage;
        }
        else
        {
            Debug.Log(friendNameText.text + " sets offline color");
            isOnline = false;
            statusImage.color = offlineImage;
        }
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
