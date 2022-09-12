using Photon.Chat;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FriendUIModel : MonoBehaviour
{
    [SerializeField] private GameObject friendUI;
    [SerializeField] private GameObject requestUI;
    [SerializeField] private Transform friendItemsContainer;
    [SerializeField] private UIFriendItem uIFriendItem;

    [SerializeField] private Transform resquestMakeFriendContanier;
    [SerializeField] private UIRequestMakeFriendItem uIRequestMakeFriendItem;
    [SerializeField] private GameObject[] requestMakeFriendImage;
    
    private ChatClient chatClient;
    private List<string> friendList;
    private Dictionary<string, UIFriendItem> uiFriendItems;

    // add friend inputField
    [SerializeField] private TMP_InputField inputField;

    public GameObject FriendUI { get => friendUI; set => friendUI = value; }
    public GameObject RequestUI { get => requestUI; set => requestUI = value; }
    public Transform FriendItemsContainer { get => friendItemsContainer; set => friendItemsContainer = value; }
    public UIFriendItem UIFriendItem { get => uIFriendItem; set => uIFriendItem = value; }
    public ChatClient ChatClient { get => chatClient; set => chatClient = value; }
    public List<string> FriendList { get => friendList; set => friendList = value; }
    public Dictionary<string, UIFriendItem> UiFriendItems { get => uiFriendItems; set => uiFriendItems = value; }
    public TMP_InputField InputField { get => inputField; set => inputField = value; }
    public Transform ResquestMakeFriendContanier { get => resquestMakeFriendContanier; set => resquestMakeFriendContanier = value; }
    public UIRequestMakeFriendItem UIRequestMakeFriendItem { get => uIRequestMakeFriendItem; set => uIRequestMakeFriendItem = value; }
    public GameObject[] RequestMakeFriendImage { get => requestMakeFriendImage; set => requestMakeFriendImage = value; }

    private void Awake()
    {
        friendList = new List<string>();
        uiFriendItems = new Dictionary<string, UIFriendItem>();
    }
}
