using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// controller
public class DecoratorUIController : MonoBehaviour
{
    [SerializeField]
    DecoratorUIModel decoratorUIModel;

    internal void IsOpen(bool v)
    {
        if (v)
        {
            decoratorUIModel.IsOpen = !decoratorUIModel.IsOpen;
            decoratorUIModel.CanvasGroup.alpha = (decoratorUIModel.IsOpen) ? 1 : 0;
            decoratorUIModel.CanvasGroup.blocksRaycasts = (decoratorUIModel.IsOpen) ? true : false;
            decoratorUIModel.PlayerController.UIOpenOrClose(decoratorUIModel.IsOpen);
        }
    }

    public void changebulletImapctPrefab(Color color)
    {
        decoratorUIModel.IsOpen = false;
        decoratorUIModel.PlayerController.UIOpenOrClose(decoratorUIModel.IsOpen);
        decoratorUIModel.CanvasGroup.alpha = 0;
        decoratorUIModel.CanvasGroup.blocksRaycasts = false;
        foreach(SingleShotGunController singleShotGunController in decoratorUIModel.PlayerController.PlayerModel.SingleShotGunController)
        {
            singleShotGunController.changeBulletImapct(color, singleShotGunController.gunModel.itemName);
        }
        //riffle.changeBulletImapct(color, "RIFFLE");
        //pistol.changeBulletImapct(color, "PISTOL");
    }
}
