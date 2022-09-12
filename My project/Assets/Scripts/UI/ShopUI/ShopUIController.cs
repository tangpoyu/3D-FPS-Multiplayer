using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// to do workflow of process
public class ShopUIController : MonoBehaviour
{
    [SerializeField] ShopUIModel shopUIModel;

    internal void GetShopItemsData()
    {
        var request = new GetCatalogItemsRequest { };
        PlayFabClientAPI.GetCatalogItems(request, OnGetCatalogItemsSuccess, OnFailure);
    }

    private void OnGetCatalogItemsSuccess(GetCatalogItemsResult obj)
    {
        List<string> bulletImpactShopItems = new List<string>();

        foreach (CatalogItem catalogItem in obj.Catalog)
        {
            if (catalogItem.ItemId == "Bullet Impact ShopItems")
            {
                bulletImpactShopItems = catalogItem.Bundle.BundledItems;
            }
        }

        foreach (string bulletImpact in bulletImpactShopItems)
        {
            foreach (CatalogItem catalogItem1 in obj.Catalog)
            {
                if (catalogItem1.ItemId == bulletImpact)
                {
                    string itemId = catalogItem1.ItemId;
                    uint cost;
                    catalogItem1.VirtualCurrencyPrices.TryGetValue("VC", out cost);
                    string[] d = catalogItem1.CustomData.Split('\"');
                    string hex = d[1];
                    Color color = HexToColor.GetColorFromString(hex);
                    GameObject shopItem = Instantiate(shopUIModel.ShopItemPrefab, shopUIModel.ShopItemContanier);
                    shopItem.GetComponent<ShopItem_VC>().initialize(itemId, cost.ToString(), color);
                    shopUIModel.ItemId_shopItem.Add(itemId, shopItem);
                }
            }
        }

        UpdateShopItemUI();
    }

    internal void UpdateShopItemUI()
    {
        var request = new GetUserInventoryRequest();
        PlayFabClientAPI.GetUserInventory(request, onGetUserInventorySuccess, OnFailure);
    }

    private void onGetUserInventorySuccess(GetUserInventoryResult obj)
    {
        foreach (ItemInstance itemInstance in obj.Inventory)
        {
            if (shopUIModel.ItemId_shopItem.TryGetValue(itemInstance.ItemId, out GameObject shopItem))
            {
                shopItem.GetComponent<ShopItem_VC>().BuyButton.interactable = false;
                shopItem.GetComponent<ShopItem_VC>().ShopItemImage.color = HexToColor.GetColorFromString("BEFF4A");
            }
        }
    }

    internal void GetCurrency()
    {
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnFailure);
    }

    private void OnGetUserInventorySuccess(GetUserInventoryResult obj)
    {
        int coins = obj.VirtualCurrency["VC"];
        shopUIModel.VirtualCurrency.text = coins.ToString();
    }

    internal void BuyFailure(string obj)
    {
        shopUIModel.ErrorUI.SetActive(true);
        shopUIModel.ErrorUI.GetComponent<BuyErrorUI>().ErrorMessage.text = obj;
    }

    private void OnFailure(PlayFabError obj)
    {
        Debug.Log($"Get Data Failed + {obj.GenerateErrorReport()}");
    }

    internal void ExitShopUI()
    {
        shopUIModel.ShopUI.SetActive(false);
    }

    
}
