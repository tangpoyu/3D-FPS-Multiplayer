using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public abstract class Gun : Item
{
    [SerializeField] protected Camera cam;

    [SerializeField]
    protected GameObject bulletImapctPrefab;

    protected void Awake()
    {
        base.Awake();
        if (myPhotonView.IsMine) return;
        // update other player's bulletImpact stat.
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            if(player == myPhotonView.Owner)
            {
                if (player.CustomProperties.ContainsKey(itemInfo.ItemName))
                {
                    float[] colors = (float[])player.CustomProperties[itemInfo.ItemName];
                    Color color = new Color(colors[0], colors[1], colors[2], colors[3]);
                    GameObject bulletImpact;
                    PrefabManager.instance.Color_bulletImpact.TryGetValue(color, out bulletImpact);
                    bulletImapctPrefab = bulletImpact;
                }
            }
           
        }
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
        //photonView.RPC(nameof(changeBulletImpact_RPC), RpcTarget.All, color.r, color.g, color.b, color.a);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (!(targetPlayer == myPhotonView.Owner)) return;
             
        if(changedProps.ContainsKey(itemInfo.ItemName))
        {
            float[] colors = (float[])changedProps[itemInfo.ItemName];
            Color color = new Color(colors[0], colors[1], colors[2], colors[3]);
            GameObject bulletImpact;
            PrefabManager.instance.Color_bulletImpact.TryGetValue(color, out bulletImpact);
            bulletImapctPrefab = bulletImpact;
        }
    }

    //public override void OnPlayerEnteredRoom(Player newPlayer)
    //{
    //    foreach(Player player in PhotonNetwork.)
    //    if (newPlayer.CustomProperties.TryGetValue(itemInfo.ItemName, out object colors))
    //    {
    //        float[] _colors = (float[])colors;
    //        Color color = new Color(_colors[0], _colors[1], _colors[2], _colors[3]);
    //        GameObject bulletImpact;
    //        PrefabManager.instance.Color_bulletImpact.TryGetValue(color, out bulletImpact);
    //        bulletImapctPrefab = bulletImpact;
    //    }
    //}

    //[PunRPC]
    //public void changeBulletImpact_RPC(float r, float g, float b, float a)
    //{

    //    Color color = new Color(r, g, b, a);
    //    GameObject bulletImpact;
    //    PrefabManager.instance.Color_bulletImpact.TryGetValue(color, out bulletImpact);
    //    bulletImapctPrefab = bulletImpact;
    //}
}
