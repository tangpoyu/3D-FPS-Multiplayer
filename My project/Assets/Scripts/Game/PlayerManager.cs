using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


// View
public class PlayerManager : MonoBehaviour
{
    private PhotonView pv;
    private GameObject player;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        print("");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (pv.IsMine)
        {
            CreateController();
        }
        else
        {
            print("");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateController()
    {
       
        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerHolder"), new Vector3(0,0, UnityEngine.Random.Range(-4,2)), Quaternion.identity, 0, new object[] { pv.ViewID });
    }

    internal void Die()
    {
        player.transform.Find("Camera").gameObject.SetActive(true);
        PhotonNetwork.Destroy(player.transform.Find("Player").GetComponent<PhotonView>());
        // CreateController();
    }
}
