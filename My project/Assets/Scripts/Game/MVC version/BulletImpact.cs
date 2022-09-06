using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletImpact : MonoBehaviour
{
    [SerializeField]
    private string name;
    private PhotonView myPV;
    private PhotonView parentPV;
  

    public string Name { get => name; set => name = value; }
    public PhotonView ParentPV { get => parentPV; set => parentPV = value; }


    private void Awake()
    {
        myPV = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (parentPV != null)
        {
            StartCoroutine(playerisDied());
        }
    }

    IEnumerator playerisDied()
    {
        yield return new WaitForSeconds(0.01f);
        if (parentPV == null)
        {
            PhotonNetwork.Destroy(myPV);
            Destroy(gameObject);
            yield break;
        }
        StartCoroutine(playerisDied());
    }
}
