using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreateRoomMenuController : MonoBehaviour
{
    [SerializeField] TMP_InputField roomNameText;
    [SerializeField] GameObject LoadingMenu;


    public void Create()
    {
        Laucher.instance.CreateRoom(roomNameText.text);  // service [ Laucher ]
        this.gameObject.SetActive(false);
        LoadingMenu.SetActive(true);
    }
}
