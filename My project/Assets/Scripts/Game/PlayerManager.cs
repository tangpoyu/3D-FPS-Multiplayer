using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;


// View
public class PlayerManager : MonoBehaviour
{
    private PhotonView pv;
    private GameObject player;
    private int kill;
    private int death;

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
        death++;
        Hashtable hash = new Hashtable();
        hash.Add("death", death);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        Invoke(nameof(Respawn), 10);
    }

    public void Respawn()
    {
        Destroy(player);
        CreateController();
    }

    public void GetKill()
    {
        pv.RPC(nameof(RPC_GetKill), pv.Owner);
    }

    [PunRPC]
    void RPC_GetKill()
    {
        kill++;
        Hashtable hash = new Hashtable();
        hash.Add("kill", kill);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }

    public static PlayerManager Find(Player player)
    {
        return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.pv.Owner == player);
    }
}
