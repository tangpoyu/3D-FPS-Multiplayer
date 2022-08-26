using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemInfo itemInfo;
    [SerializeField]
    private GameObject itemGameObject;

    public GameObject ItemGameObject { get => itemGameObject; set => itemGameObject = value; }
}
