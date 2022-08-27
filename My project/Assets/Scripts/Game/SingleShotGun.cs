using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGun : Gun
{
    [SerializeField] Camera cam;
    PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public override void use()
    {
        Shoot();
    }

    private void Shoot()
    {

        // LEARN
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(((GunInfo)itemInfo).Damage);
            photonView.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
        }
    }

    [PunRPC]
    public void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if(colliders.Length != 0)
        {
            GameObject bulletImapct = Instantiate(bulletImapctPrefab,
               hitPosition + hitNormal * 0.001f,
               Quaternion.LookRotation(hitNormal) * bulletImapctPrefab.transform.rotation);
            Destroy(bulletImapct, 10f);
            bulletImapct.transform.SetParent(colliders[0].transform);
        }
        // Learn
     
    }
}
