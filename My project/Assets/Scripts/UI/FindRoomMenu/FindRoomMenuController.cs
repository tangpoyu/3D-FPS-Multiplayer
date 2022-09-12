using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class FindRoomMenuController : MonoBehaviour
{
    [SerializeField] GameObject TitleMenu, loadingMenu, roomListPrefab;
    [SerializeField] Transform roomListContent;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonMasterServerConnector.instance.IsRoomListUpdated)
        {
            foreach (Transform trans in roomListContent)
            {
                Destroy(trans.gameObject);
            }
            Dictionary<string, RoomInfo>.ValueCollection roomInfos = PhotonMasterServerConnector.instance.RoomName_RoomInfo.Values;
            foreach (RoomInfo roomInfo in roomInfos)
            {
                GameObject obj = Instantiate(roomListPrefab, roomListContent);
                obj.GetComponentInChildren<TMP_Text>().text = roomInfo.Name;
            }
            PhotonMasterServerConnector.instance.IsRoomListUpdated = false;
        }
    }

    public void Back()
    {
        PhotonMasterServerConnector.instance.IsJoinedLobby = true;
        TitleMenu.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void enterRoom()
    {
        PhotonMasterServerConnector.instance.JoinRoom(EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TMP_Text>().text); // service
        this.gameObject.SetActive(false);
        loadingMenu.SetActive(true); // view
    }
}
