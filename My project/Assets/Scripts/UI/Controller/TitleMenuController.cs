using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controller
public class TitleMenuController : MonoBehaviour
{
    [SerializeField] GameObject findRoomMenu,  creatRoomMenu, FriendUI, Image; // view [ createRoomMenu , findRoomMenu ]

    private void Update()
    {
        // service [ Laucher ]
        if (Laucher.instance.IsJoinedLobby || Laucher.instance.IsInLogin)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
      
        if(Laucher.instance.IsInLogin == false)
        {
            Image.gameObject.SetActive(false);
        }
    }

    public void findRoom()
    {
        Laucher.instance.IsJoinedLobby = false;
        findRoomMenu.SetActive(true);
    }

    public void createRoom()
    {
        Laucher.instance.IsJoinedLobby = false;
        creatRoomMenu.SetActive(true);
    }

    public void OpenFriendUI()
    {
        FriendUI.SetActive(true);
    }
}
