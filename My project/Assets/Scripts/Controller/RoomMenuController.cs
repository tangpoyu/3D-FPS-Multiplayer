using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// controller
public class RoomMenuController : MonoBehaviour
{
    [SerializeField] TMP_Text roomName;
    [SerializeField] GameObject LoadingMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Laucher.instance.IsJoinedRoom)  // service [ Laucher ]
        {
            roomName.text = Laucher.instance.GetCurrentRoomName();  // service [ Laucher ]
        }
        else
        {
            this.gameObject.SetActive(false); // view [ RoomMenu ] 
        }
    }

    public void LeaveRoom()
    {
        Laucher.instance.LeaveRoom(); // service [ Laucher ]
        LoadingMenu.SetActive(true);
    }
}
