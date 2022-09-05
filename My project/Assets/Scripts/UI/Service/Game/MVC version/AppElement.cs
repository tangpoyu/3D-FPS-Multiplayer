using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppElement : MonoBehaviourPunCallbacks
{
    public Application app { get { return GameObject.FindObjectOfType<Application>(); } }
}
