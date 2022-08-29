using Photon.Pun;
using UnityEngine;

public abstract class Gun : Item
{
    [SerializeField] protected Camera cam;

    protected GameObject bulletImapctPrefab;
    
    [SerializeField]
    protected GameObject  bulletImapctPrefab_pink, bulletImapctPrefab_orange,
        bulletImapctPrefab_yellow, bulletImapctPrefab_green,  bulletImapctPrefab_blue,
        bulletImapctPrefab_purple;

    protected void Awake()
    {
        base.Awake();
        bulletImapctPrefab = bulletImapctPrefab_pink;
    }

    public void changeBulletImapct(string color)
    {
        photonView.RPC(nameof(changeBulletImpact_RPC), RpcTarget.All, color);
    }

    [PunRPC]
    public void changeBulletImpact_RPC(string color)
    {
        switch (color)
        {
            case "pink":
                bulletImapctPrefab = bulletImapctPrefab_pink;
                break;

            case "orange":
                bulletImapctPrefab = bulletImapctPrefab_orange;
                break;

            case "yellow":
                bulletImapctPrefab = bulletImapctPrefab_yellow;
                break;

            case "green":
                bulletImapctPrefab = bulletImapctPrefab_green;
                break;

            case "blue":
                bulletImapctPrefab = bulletImapctPrefab_blue;
                break;

            case "purple":
                bulletImapctPrefab = bulletImapctPrefab_purple;
                break;
        }
    }
    
}
