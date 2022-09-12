using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TO detect 
public class ShopUIView : MonoBehaviour
{
    [SerializeField] private ShopUIController shopUIController;

    private void Awake()
    {
        PlayfabConnector.playFabConnected += GetShopItemsData;
        PlayfabConnector.playFabConnected += GetCurrency;
        ShopItem_VC.BuySucess += BuySucess;
        ShopItem_VC.BuyFailure += BuyFailure;
    }
    private void OnDestroy()
    {
        PlayfabConnector.playFabConnected -= GetShopItemsData;
        PlayfabConnector.playFabConnected -= GetCurrency;
        ShopItem_VC.BuySucess -= BuySucess;
        ShopItem_VC.BuyFailure -= BuyFailure;
    }

  

    private void GetShopItemsData()
    {
        shopUIController.GetShopItemsData();
    }

    private void GetCurrency()
    {
        shopUIController.GetCurrency();
    }

    private void BuySucess()
    {
        GetCurrency();
      //  shopUIController.UpdateShopUI();
    }

    private void BuyFailure(string obj)
    {
        shopUIController.BuyFailure(obj);
    }


    public void ExitShopUI()
    {
        shopUIController.ExitShopUI();
    }
}
