using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// controller
public class TitleMenuController : MonoBehaviour
{
    [SerializeField] GameObject findRoomMenu,  creatRoomMenu, FriendUI, PartyUI, Image; // view [ createRoomMenu , findRoomMenu ]
    [SerializeField] Button friendUIButton, partyUIButton;

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
            friendUIButton.interactable = true;
            partyUIButton.interactable = true;
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

    public void OpenFriendUI()
    {
        FriendUI.SetActive(true);
    }

    public void OpenPartyUI()
    {
        PartyUI.SetActive(true);
    }
}
