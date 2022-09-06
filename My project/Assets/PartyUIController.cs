using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyUIController : MonoBehaviour
{
    [SerializeField] private PartyUIModel partyUIModel;

    internal void updatePartyUI(string sender, string message)
    {
        UIInviteItem uIInviteItem = Instantiate(partyUIModel.UIInviteItem, partyUIModel.InviteItemContanier);
        uIInviteItem.Initialize(sender, message);
        partyUIModel.Invites.Add(uIInviteItem);
    }

    internal void AccepteInvite(UIInviteItem invite)
    {
        if (partyUIModel.Invites.Contains(invite))
        {
            partyUIModel.Invites.Remove(invite);
            Destroy(invite.gameObject);
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
}
