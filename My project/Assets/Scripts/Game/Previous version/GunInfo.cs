using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FPS/New Gun")]
public class GunInfo : ItemInfo
{
    [SerializeField]
    private float damage;

    public float Damage { get => damage; set => damage = value; }

    public GunInfo(string itemName, float damage) : base(itemName)
    {
        this.damage = damage;
    }
}

