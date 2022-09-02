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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

            //if (pv.IsMine)
            //{
            //    Hashtable hash = new Hashtable();
            //    Color color;
            //    PrefabManager.instance.BulletImpact_color.TryGetValue(bulletImapct, out color);
            //    float[] colors = new float[4];
            //    colors[0] = color.r; colors[1] = color.g; color[2] = color.b; color[3] = color.a;
            //    float[] positions = new float[3];
            //    positions[0] = myPosition.x; positions[1] = myPosition.y; positions[2] = myPosition.z;
            //    float[] quaternions = new float[4];
            //    quaternions[0] = myQuaternion.x; quaternions[1] = myQuaternion.y;
            //    quaternions[2] = myQuaternion.z; quaternions[3] = myQuaternion.w;

            //   // data.Add("colors",(object[]) colors);
            //    hash.Add("bulletImpact", bulletImapct);
            //    PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            //}

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
