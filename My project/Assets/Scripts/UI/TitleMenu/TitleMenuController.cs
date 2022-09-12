using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// controller
public class TitleMenuController : MonoBehaviour
{
    [SerializeField] GameObject loadingMenu, findRoomMenu,  creatRoomMenu, ShopUI, PartyUI, FriendUI,  Image; // view [ createRoomMenu , findRoomMenu ]
    [SerializeField] Button shopUIButton, partyUIButton, friendUIButton;

    private void Awake()
    {
        
    }

    private void Update()
    {
        // service [ Laucher ]
        if (PhotonMasterServerConnector.instance.IsJoinedLobby || PhotonMasterServerConnector.instance.IsInLogin)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
      
        if(PhotonMasterServerConnector.instance.IsInLogin == false)
        {
            Image.gameObject.SetActive(false);
            shopUIButton.interactable = true;
            partyUIButton.interactable = true;
            friendUIButton.interactable = true;
        }

        if (PhotonMasterServerConnector.instance.IsDirectJoinRoom)
        {
            PhotonMasterServerConnector.instance.IsJoinedLobby = false;
            PhotonMasterServerConnector.instance.IsLoad = true;
            loadingMenu.SetActive(true);
            PhotonMasterServerConnector.instance.IsDirectJoinRoom = false;
        }
    }

    public void findRoom()
    {
        PhotonMasterServerConnector.instance.IsJoinedLobby = false;
        findRoomMenu.SetActive(true);
    }

    public void createRoom()
    {
        PhotonMasterServerConnector.instance.IsJoinedLobby = false;
        creatRoomMenu.SetActive(true);
    }

    public void OpenShopUI()
    {
        ShopUI.SetActive(true);
    }

    public void OpenPartyUI()
    {
        PartyUI.SetActive(true);
    }

    public void OpenFriendUI()
    {
        FriendUI.SetActive(true);
    }
}
