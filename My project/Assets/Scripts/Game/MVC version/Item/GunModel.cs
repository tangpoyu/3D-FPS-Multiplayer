using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunModel : ItemModel
{
    [SerializeField]
    internal GameObject bulletImpactPrefeb;
    [SerializeField]
    private float damage;

    public float Damage { get => damage; set => damage = value; }

    protected void Awake()
    {
        base.Awake();
        if (pv.IsMine) return;
        foreach(Player player in PhotonNetwork.PlayerList)
        {
            if (player == pv.Owner)
            {
                if (player.CustomProperties.ContainsKey(itemName))
                {
                    float[] colors = (float[])player.CustomProperties[itemName];
                    Color color = new Color(colors[0], colors[1], colors[2], colors[3]);
                    GameObject bulletImpact;
                    PrefabManager.instance.Color_bulletImpact.TryGetValue(color, out bulletImpact);
                    bulletImpactPrefeb = bulletImpact;
                }
            }

        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (!(targetPlayer == pv.Owner)) return;
        if(changedProps.ContainsKey(itemName))
        {
            float[] colors = (float[])changedProps[itemName];
            Color color = new Color(colors[0], colors[1], colors[2], colors[3]);
            GameObject bulletImpact;
            PrefabManager.instance.Color_bulletImpact.TryGetValue(color, out bulletImpact);
            bulletImpactPrefeb = bulletImpact;
        }
    }
}
