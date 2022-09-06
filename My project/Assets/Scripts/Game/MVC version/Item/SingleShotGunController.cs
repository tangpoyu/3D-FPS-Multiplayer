using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class SingleShotGunController : GunController
{

    private void Awake()
    {
        base.Awake();
    }

    public override void use()
    {
        Ray ray = gunModel.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = gunModel.transform.position;
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            hit.collider.gameObject.GetComponent<IDamageable>()?.TakeDamage(gunModel.Damage);
            pv.RPC("RPC_Shoot", pv.Owner, hit.point, hit.normal);
        }
    }

    [PunRPC]
    public void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if (colliders.Length != 0)
        {
            Vector3 myPosition = hitPosition + hitNormal * 0.001f;
            Quaternion myQuaternion = Quaternion.LookRotation(hitNormal) * gunModel.bulletImpactPrefeb.transform.rotation;
            string name = gunModel.bulletImpactPrefeb.GetComponent<BulletImpact>().Name;
            string path = "";
            switch (name)
            {
                case "BLUE":
                    path = Path.Combine("PhotonPrefabs", "BulletImpact_BLUE");
                    break;

                case "GREEN":
                    path = Path.Combine("PhotonPrefabs", "BulletImpact_GREEN");
                    break;

                case "ORANGE":
                    path = Path.Combine("PhotonPrefabs", "BulletImpact_ORANGE");
                    break;

                case "PINK":
                     path = Path.Combine("PhotonPrefabs", "BulletImpact_PINK");
                    break;

                case "PURPLE":
                    path = Path.Combine("PhotonPrefabs", "BulletImpact_PURPLE");
                    break;

                case "YELLOW":
                    path = Path.Combine("PhotonPrefabs", "BulletImpact_YELLOW");
                    break;
            }
            GameObject bulletImpact = PhotonNetwork.Instantiate(path, myPosition, myQuaternion);

            StartCoroutine(BulletImpactDestory(bulletImpact));
            if(!(colliders[0].gameObject.GetComponent<IDamageable>() == null))
            {
                bulletImpact.GetComponent<BulletImpact>().ParentPV = colliders[0].gameObject.GetComponent<PhotonView>();
            }
        }
    }

    IEnumerator BulletImpactDestory(GameObject bulletImpact)
    {
        yield return new WaitForSeconds(10);
        if(bulletImpact != null)
        {
            PhotonNetwork.Destroy(bulletImpact.GetComponent<PhotonView>());
            Destroy(bulletImpact);
        }
    }
}
