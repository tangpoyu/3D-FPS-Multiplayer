using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyUIController : MonoBehaviour
{
    [SerializeField] private PartyUIModel partyUIModel;

    internal void updatePartyUI(string sender, string roomName)
    {
        UIInviteItem uIInviteItem = Instantiate(partyUIModel.UIInviteItem, partyUIModel.InviteItemContanier);
        uIInviteItem.Initialize(sender, roomName);
        foreach(GameObject image in partyUIModel.HaveInviteImage)
        {
            image.SetActive(true);
        }
        partyUIModel.Invites.Add(uIInviteItem);
    }

    internal void AccepteInvite(UIInviteItem invite)
    {
        if (partyUIModel.Invites.Contains(invite))
        {
            partyUIModel.Invites.Remove(invite);
            Destroy(invite.gameObject);
            //  Debug.Log("open the loding menu");
            //  partyUIModel.LodingMenu.SetActive(true);
            foreach (GameObject image in partyUIModel.HaveInviteImage)
            {
                image.SetActive(false);
            }
            partyUIModel.PartyUI.SetActive(false);
        }
    }

    internal void DeclineInvite(UIInviteItem invite)
    {
        if (partyUIModel.Invites.Contains(invite))
        {
            partyUIModel.Invites.Remove(invite);
            Destroy(invite.gameObject);
        }
    }

    internal void Exit()
    {
        partyUIModel.PartyUI.SetActive(false);
        foreach (GameObject image in partyUIModel.HaveInviteImage)
        {
            image.SetActive(false);
        }
    }
}
