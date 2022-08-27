using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    protected ItemInfo itemInfo;
    [SerializeField]
    private GameObject itemGameObject;

    public GameObject ItemGameObject { get => itemGameObject; set => itemGameObject = value; }

    public abstract void use();
}
