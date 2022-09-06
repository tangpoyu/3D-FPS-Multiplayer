using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyUIModel : MonoBehaviour
{
    [SerializeField] private Transform inviteItemContanier;
    [SerializeField] private UIInviteItem uIInviteItem;

  
    private List<UIInviteItem> invites;

    public Transform InviteItemContanier { get => inviteItemContanier; set => inviteItemContanier = value; }
    public UIInviteItem UIInviteItem { get => uIInviteItem; set => uIInviteItem = value; }
   
    public List<UIInviteItem> Invites { get => invites; set => invites = value; }

    private void Awake()
    {
        Invites = new List<UIInviteItem>();
    }
}
