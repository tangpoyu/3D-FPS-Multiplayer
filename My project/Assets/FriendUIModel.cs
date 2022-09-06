using Photon.Chat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendUIModel : MonoBehaviour
{
    [SerializeField] private GameObject friendUI;
    [SerializeField] private Transform friendItemsContainer;
    [SerializeField] private UIFriendItem uIFriendItem;
    private ChatClient chatClient;
    private List<string> friendList;
    private Dictionary<string, UIFriendItem> uiFriendItems;

    public GameObject FriendUI { get => friendUI; set => friendUI = value; }
    public Transform FriendItemsContainer { get => friendItemsContainer; set => friendItemsContainer = value; }
    public UIFriendItem UIFriendItem { get => uIFriendItem; set => uIFriendItem = value; }
    public ChatClient ChatClient { get => chatClient; set => chatClient = value; }
    public List<string> FriendList { get => friendList; set => friendList = value; }
    public Dictionary<string, UIFriendItem> UiFriendItems { get => uiFriendItems; set => uiFriendItems = value; }

    private void Awake()
    {
        friendList = new List<string>();
        uiFriendItems = new Dictionary<string, UIFriendItem>();
    }
}
