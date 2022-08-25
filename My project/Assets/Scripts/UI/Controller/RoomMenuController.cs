using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// controller
public class RoomMenuController : MonoBehaviour
{
    [SerializeField] TMP_Text roomName;
    [SerializeField] GameObject LoadingMenu, playerListItem, StartButton;
    [SerializeField] Transform playerListContent;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Laucher.instance.IsPlayerEnterRoomOrLeave)
        {
            foreach(Transform trans in playerListContent)
            {
                Destroy(trans.gameObject);
            }

            foreach(Player player in Laucher.instance.Players)
            {
                Instantiate(playerListItem, playerListContent).GetComponent<PlayerListItem>().SetUp(player);
            }
            Laucher.instance.IsPlayerEnterRoomOrLeave = false;
        }

        if (Laucher.instance.IsCreatedRoom || Laucher.instance.IsJoinedRoom)
        {
            roomName.text = Laucher.instance.GetCurrentRoomName();  // service [ Laucher ]
        }
           
        if (Laucher.instance.IsCreatedRoom == false && Laucher.instance.IsJoinedRoom == false)
        {
            this.gameObject.SetActive(false); // view [ RoomMenu ] 
        }

        if (Laucher.instance.isMasterClient())
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
        Laucher.instance.LeaveRoom(); // service [ Laucher ]
        LoadingMenu.SetActive(true);
    }

    public void StartGame()
    {
        Laucher.instance.StartGame();
    }
}
