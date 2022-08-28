using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    [SerializeField] TMP_InputField username;
    [SerializeField] GameObject image;

    private void Start()
    {
        if(PlayerPrefs.HasKey("username"))
        {
            username.text = PlayerPrefs.GetString("username");
        }
        else
        {
            username.text = "Player" + Random.Range(0, 10000).ToString("00000"); 
        }
    }

    public void UserNameInput()
    {
        PhotonNetwork.NickName = username.text;
        PlayerPrefs.SetString("username", username.text);
        image.SetActive(false);
    }
}
