using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using System;
using Photon.Realtime;
using ExitGames.Client.Photon;
using System.Collections.Generic;

// service
public class PhotonChatConnector : MonoBehaviour, IChatClientListener
{
    public static Action<string, string> OnRoomInvite = delegate { };
    public static Action<ChatClient> OnChatConnected = delegate { };
    public static Action<PhotonStatus> OnStatusUpdated = delegate { };
    public static Action<string> OnMakeFriendRequest = delegate { };
    public static Action<string> OnAddPlayFabFriend = delegate { };
    public static Action<string> OnRemoveFriend = delegate { };

    private string nickname;
    private ChatClient chatClient;
   

    private void Awake()
    {
        PlayfabConnector.playFabConnected += ConnectToPhotonChat;
        UIFriendItem.OnInviteFriend += HandleFriendInvite;
        FriendUIController.OnAddFriend += HandleRequestMakePlayfabFriend;
        UIRequestMakeFriendItem.OnAcceptInvite += HandleAcceptMakeFriend;
        UIFriendItem.OnRemoveFriend += HandleRemoveFriend;
        chatClient = new ChatClient(this);
    }



    private void OnDestroy()
    {
        PlayfabConnector.playFabConnected -= ConnectToPhotonChat;
        UIFriendItem.OnInviteFriend -= HandleFriendInvite;
        FriendUIController.OnAddFriend -= HandleRequestMakePlayfabFriend;
        UIRequestMakeFriendItem.OnAcceptInvite -= HandleAcceptMakeFriend;
        UIFriendItem.OnRemoveFriend -= HandleRemoveFriend;
    }

    // Update is called once per frame
    void Update()
    {
        chatClient.Service();
    }

    private void ConnectToPhotonChat()
    {
        Debug.Log("To connect photon chat.");
        nickname = PlayerPrefs.GetString("USERNAME");
        // To explicitly[Photon.Chat.AuthenticationValues] is beacause Photon.Pun also has[AuthenticationValues]
        chatClient.AuthValues = new Photon.Chat.AuthenticationValues(nickname);
        ChatAppSettings chatSettings = GetChatSettings(PhotonNetwork.PhotonServerSettings.AppSettings);
        chatClient.ConnectUsingSettings(chatSettings);
    }

    public void OnConnected()
    {
        Debug.Log("Connected to Photon Chat");
        chatClient.SetOnlineStatus(ChatUserStatus.Online);
        OnChatConnected?.Invoke(chatClient);
        // FOR TEST
        // SendDirectMesage("cena", "hi, cena."); 
    }

    // /////// [ For Test ] ////////////////////////
    //public void SendDirectMesage(string recipient, string message)
    //{
    //    chatClient.SendPrivateMessage(recipient, message);
    //}


    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        if (chatClient.UserId == sender) return;
        
        string[] m = ((string)message).Split(",");
        switch (m[0])
        {
            case "INVITE JOIN ROOM":
                OnRoomInvite?.Invoke(sender, m[1]);
                Debug.Log($"{sender} invites {chatClient.UserId} to {m[1]} room.");
                break;

            case "MAKE FRIEND":
                OnMakeFriendRequest?.Invoke(sender);
                Debug.Log($"{sender} request make friend with {chatClient.UserId}.");
                break;

            case "ADD FRIEND":
                OnAddPlayFabFriend?.Invoke(m[1]);
                break;

            case "REMOVE FRIEND":
                OnRemoveFriend?.Invoke(sender);
                break;
        }
    }

    public void OnDisconnected()
    {
        Debug.Log("Disconnected from Photon Chat");
        chatClient.SetOnlineStatus(ChatUserStatus.Offline);
    }

    ////////////////////// handle friend invite //////////////////////////////////////////////////////
    private void HandleFriendInvite(string receipient)
    {
        string message = "INVITE JOIN ROOM," + PhotonNetwork.CurrentRoom.Name;
        chatClient.SendPrivateMessage(receipient, message);
    }

    private void HandleRequestMakePlayfabFriend(string name)
    {
        List<string> list = new List<string>();
        string message = "MAKE FRIEND," + name;
        chatClient.SendPrivateMessage(name, message);
    }

    private void HandleAcceptMakeFriend(UIRequestMakeFriendItem obj)
    {
        string message = "ADD FRIEND," + chatClient.UserId;
        chatClient.SendPrivateMessage(obj.SenderName, message);
    }

    private void HandleRemoveFriend(string obj)
    {
        string message = "REMOVE FRIEND," + chatClient.UserId;
        chatClient.SendPrivateMessage(obj, message);
    }

    // which will be called when chatClient.Addfriends, and when has a channel between two players. 
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        Debug.Log(user + " updates " + status + " status.");
        PhotonStatus photonStatus = new PhotonStatus(user, status, (string)message);
        OnStatusUpdated?.Invoke(photonStatus);
    }

    public void DebugReturn(DebugLevel level, string message)
    {
       
    }

    public void OnChatStateChange(ChatState state)
    {
       
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {

    }


    public void OnSubscribed(string[] channels, bool[] results)
    {
    }

    public void OnUnsubscribed(string[] channels)
    {
    }

  

    public void OnUserSubscribed(string channel, string user)
    {
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
    }


    public  ChatAppSettings GetChatSettings(AppSettings appSettings)
    {
        return new ChatAppSettings
        {
            AppIdChat = appSettings.AppIdChat,
            AppVersion = appSettings.AppVersion,
            FixedRegion = appSettings.IsBestRegion ? null : "asia",
            NetworkLogging = appSettings.NetworkLogging,
            Protocol = appSettings.Protocol,
            EnableProtocolFallback = appSettings.EnableProtocolFallback,
            Server = appSettings.IsDefaultNameServer ? null : appSettings.Server,
            Port = (ushort)appSettings.Port,
            ProxyServer = appSettings.ProxyServer
            // values not copied from AppSettings class: AuthMode
            // values not needed from AppSettings class: EnableLobbyStatistics 
        };
    }

}
