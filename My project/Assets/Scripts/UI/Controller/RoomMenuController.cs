using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// controller
public class RoomMenuController : MonoBehaviour
{
    [SerializeField] TMP_Text roomName;
    [SerializeField] GameObject LoadingMenu, friendUI, playerListItem, StartButton;
    [SerializeField] Transform playerListContent;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (PhotonMasterServerConnector.instance.IsPlayerEnterRoomOrLeave)
        {
            foreach(Transform trans in playerListContent)
            {
                Destroy(trans.gameObject);
            }

            foreach(Player player in PhotonMasterServerConnector.instance.Players)
            {
                Instantiate(playerListItem, playerListContent).GetComponent<PlayerListItem>().SetUp(player);
            }
            PhotonMasterServerConnector.instance.IsPlayerEnterRoomOrLeave = false;
        }

        if (PhotonMasterServerConnector.instance.IsCreatedRoom || PhotonMasterServerConnector.instance.IsJoinedRoom)
        {
            roomName.text = PhotonMasterServerConnector.instance.GetCurrentRoomName();  // service [ Laucher ]
        }
           
        if (PhotonMasterServerConnector.instance.IsCreatedRoom == false && PhotonMasterServerConnector.instance.IsJoinedRoom == false)
        {
            this.gameObject.SetActive(false); // view [ RoomMenu ] 
        }

        if (PhotonMasterServerConnector.instance.isMasterClient())
        {
            StartButton.SetActive(true);
        }
        else
        {
            StartButton.SetActive(false);
        }
    }

    public void LeaveRoom()
    {
        PhotonMasterServerConnector.instance.LeaveRoom(); // service [ Laucher ]
        LoadingMenu.SetActive(true);
    }

    public void StartGame()
    {
        PhotonMasterServerConnector.instance.StartGame();
    }

    public void OpenFriendUI()
    {
        friendUI.SetActive(true);
    }
}
