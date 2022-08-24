using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controller
public class TitleMenuController : MonoBehaviour
{
    [SerializeField] GameObject creatRoomMenu; // view [ createRoomMenu ]

    private void Update()
    {
        this.gameObject.SetActive(Laucher.instance.IsJoinedLobby ? true : false); // service [ Laucher ]
    }

    public void createRoom()
    {
        Laucher.instance.IsJoinedLobby = false;
        creatRoomMenu.SetActive(true);
    }
}
