using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Item
{
    [SerializeField]
    protected GameObject bulletImapctPrefab;

    public override void use()
    {
        throw new System.NotImplementedException();
    }
}
