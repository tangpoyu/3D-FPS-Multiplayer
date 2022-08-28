using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] PhotonView pv;
    [SerializeField] TMP_Text text;
    Camera cam;

    private void Start()
    {
        if (pv.IsMine) gameObject.SetActive(false);
        text.text = pv.Owner.NickName;
    }

    private void Update()
    {
        if (cam == null)
            cam = FindObjectOfType<Camera>();

        transform.LookAt(cam.transform);
        transform.Rotate(Vector3.up * 180);
    }
}
