using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyUIModel : MonoBehaviour
{
    [SerializeField] private GameObject partyUI;
    [SerializeField] private Transform inviteItemContanier;
    [SerializeField] private UIInviteItem uIInviteItem;
    [SerializeField] private GameObject[] haveInviteImage;
    private List<UIInviteItem> invites;

    public GameObject PartyUI { get => partyUI; set => partyUI = value; }
    // public GameObject LodingMenu { get => lodingMenu; set => lodingMenu = value; }
    public Transform InviteItemContanier { get => inviteItemContanier; set => inviteItemContanier = value; }
    public UIInviteItem UIInviteItem { get => uIInviteItem; set => uIInviteItem = value; }
    public List<UIInviteItem> Invites { get => invites; set => invites = value; }
    public GameObject[] HaveInviteImage { get => haveInviteImage; set => haveInviteImage = value; }

    private void Awake()
    {
        Invites = new List<UIInviteItem>();
    }
}
