using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyUIView : MonoBehaviour
{
    [SerializeField] private PartyUIController partyUIController;

    private void Awake()
    {
        PhotonChatConnector.OnRoomInvite += HandleRoomInvite;
        UIInviteItem.OnAcceptInvite += HandleAccepteInvite;
        UIInviteItem.OnDeclineInvite += HandleDeclineInvite;
    }

    private void OnDestroy()
    {
        PhotonChatConnector.OnRoomInvite -= HandleRoomInvite;
        UIInviteItem.OnAcceptInvite -= HandleAccepteInvite;
        UIInviteItem.OnDeclineInvite -= HandleDeclineInvite;
    }


    // methods subscribe actions will also be called when actions is invoked while methods of gameObject is inactive.
    private void HandleRoomInvite(string sender, string message)
    {
        partyUIController.updatePartyUI(sender, message);
    }

    private void HandleAccepteInvite(UIInviteItem invite)
    {
        partyUIController.AccepteInvite(invite);
    }

    private void HandleDeclineInvite(UIInviteItem invite)
    {
        partyUIController.DeclineInvite(invite);
    }

    public void Exit()
    {
        this.gameObject.SetActive(false);
    }
}
