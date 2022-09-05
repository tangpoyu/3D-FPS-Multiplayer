using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public abstract class GunController : ItemController
{
    [SerializeField]
    internal GunModel gunModel;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeBulletImapct(Color color, string type)
    {
        Hashtable hash = new Hashtable();
        float[] colors = new float[4];
        colors[0] = color.r;
        colors[1] = color.g;
        colors[2] = color.b;
        colors[3] = color.a;
        hash.Add(type, colors);
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
    }
}
