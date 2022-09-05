using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    [SerializeField] TMP_InputField username;
    

    private void Start()
    {
        if(PlayerPrefs.HasKey("USERNAME"))
        {
            username.text = PlayerPrefs.GetString("USERNAME");
        }
        else
        {
            username.text = "Player" + Random.Range(0, 10000).ToString("00000"); 
        }
    }
}
