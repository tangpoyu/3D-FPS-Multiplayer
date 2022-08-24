using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// controller
public class LoadingMenuController : MonoBehaviour
{
    [SerializeField] GameObject TitleMenu, RoomMenu, ErrorMenu;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Laucher.instance.IsLoad == false)
        {
            this.gameObject.SetActive(false);
        }
        if (Laucher.instance.IsJoinedLobby)
        {
            TitleMenu.SetActive(true);
        }
        if (Laucher.instance.IsJoinedRoom)
        {
            RoomMenu.SetActive(true);
        }
        if (Laucher.instance.IsCreateRoomFailed)
        {
            ErrorMenu.SetActive(true);
        }
    }
}
