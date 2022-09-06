using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using System;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class PhotonChatConnector : MonoBehaviour, IChatClientListener
{
    public static Action<string, string> OnRoomInvite = delegate { };
    public static Action<ChatClient> OnChatConnected = delegate { };
    public static Action<PhotonStatus> OnStatusUpdated = delegate { };


    private string nickname;
    private ChatClient chatClient;
   

    private void Awake()
    {
        PlayfabConnector.playFabLogin += ConnectToPhotonChat;
        UIFriendItem.OnInviteFriend += HandleFriendInvite;
        chatClient = new ChatClient(this);
    }

    private void OnDestroy()
    {
        PlayfabConnector.playFabLogin -= ConnectToPhotonChat;
        UIFriendItem.OnInviteFriend -= HandleFriendInvite;
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

    // Update is called once per frame
    void Update()
    {
        chatClient.Service();   
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
        if (!string.IsNullOrEmpty(message.ToString()))
        {
            string[] senderAndRecipient = channelName.Split(":");
            string senderName = senderAndRecipient[1];
            string recipient = senderAndRecipient[0];
            Debug.Log($"{sender} sends [{message}] to {recipient}.");
            OnRoomInvite?.Invoke(sender, (string) message);
        }
    }

    public void OnDisconnected()
    {
        Debug.Log("Disconnected from Photon Chat");
        chatClient.SetOnlineStatus(ChatUserStatus.Offline);
    }

    ////////////////////// Method which subscribed Action //////////////////////////////////////////////////////
    private void HandleFriendInvite(string receipient)
    {
        if (PhotonNetwork.InRoom)
        {
            chatClient.SendPrivateMessage(receipient, PhotonNetwork.CurrentRoom.Name);
        }
    }

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
            FixedRegion = appSettings.IsBestRegion ? null : appSettings.FixedRegion,
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
