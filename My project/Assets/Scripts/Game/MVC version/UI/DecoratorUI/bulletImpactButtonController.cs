using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// controller
public class bulletImpactButtonController : MonoBehaviour
{
    [SerializeField]
    bulletImapctButtonModel bulletImapctButtonModel;

    public void click()
    {
        bulletImapctButtonModel.DecoratorUI.changebulletImapctPrefab(GetComponent<Image>().color);
    }
}
