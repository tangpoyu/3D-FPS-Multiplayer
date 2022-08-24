using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controller
public class TitleMenuController : MonoBehaviour
{
    [SerializeField] GameObject findRoomMenu,  creatRoomMenu; // view [ createRoomMenu , findRoomMenu ]

    private void Update()
    {
        this.gameObject.SetActive(Laucher.instance.IsJoinedLobby ? true : false); // service [ Laucher ]
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


}
