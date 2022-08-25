using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// View
public class RoomListItem : MonoBehaviour
{
    FindRoomMenuController findRoomMenuController; // controller

    private void Awake()
    {
        findRoomMenuController = GetComponentInParent<FindRoomMenuController>();
    }

    public void enterRoom()
    {
        findRoomMenuController.enterRoom(); 
    }
}
