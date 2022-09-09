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
        Debug.Log("loadingMenu start update");
    }

    // Update is called once per frame
    void Update()
    {
        if(PhotonMasterServerConnector.instance.IsLoad == false)
        {
            this.gameObject.SetActive(false);
        }

        if (PhotonMasterServerConnector.instance.IsJoinedLobby)
        {
            TitleMenu.SetActive(true);
        }
        

        if (PhotonMasterServerConnector.instance.IsJoinedRoom || PhotonMasterServerConnector.instance.IsCreatedRoom)
        {
            RoomMenu.SetActive(true);
        }

        if (PhotonMasterServerConnector.instance.IsCreateRoomFailed)
        {
            ErrorMenu.SetActive(true);
        }
    }
}
