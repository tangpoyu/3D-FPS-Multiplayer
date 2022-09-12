using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayfabGameIntialData : MonoBehaviour
{
    public static PlayfabGameIntialData instance;
    [SerializeField] private GameObject Application;
    private List<Color> bulletImpactColorDefault;
    private List<string> bulletImpactInventory;

    public List<Color> BulletImpactColorDefault { get => bulletImpactColorDefault; set => bulletImpactColorDefault = value; }
   

    private void Awake()
    {
        bulletImpactColorDefault = new List<Color>();
        bulletImpactInventory = new List<string>();
        if (instance == null) instance = this;
        PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetUserInventorySuccess, OnFailure);
    }

    private void OnGetUserInventorySuccess(GetUserInventoryResult obj)
    {
        foreach (ItemInstance itemInstance in obj.Inventory)
        {
            // Get bullet Impact which player has.
            if (itemInstance.ItemClass == "Bullet Impact")
            {
                bulletImpactInventory.Add(itemInstance.ItemId);
            }
        }

        var request = new GetCatalogItemsRequest { };
        PlayFabClientAPI.GetCatalogItems(request, OnGetCatalogItemsSuccess, OnFailure);
    }

    private void OnGetCatalogItemsSuccess(GetCatalogItemsResult obj)
    {
        List<string> bulletImpactAll = new List<string>();
        
        
        foreach (CatalogItem catalogItem in obj.Catalog)
        {
            // Get Default bullet Impact.
            if (catalogItem.ItemId == "Bullet Impact Default")
            {
                bulletImpactAll  = catalogItem.Bundle.BundledItems;
            }
        }

        bulletImpactAll.AddRange(bulletImpactInventory);

        foreach (string bulletImpact in bulletImpactAll)
        {
            foreach (CatalogItem catalogItem1 in obj.Catalog)
            {
                if (catalogItem1.ItemId == bulletImpact)
                {
                    //string[] d = catalogItem1.CustomData.Split("\"");
                    //string[] rgba = d[3].Split(",");
                    // BulletImpactColor.Add(new Color(float.Parse(rgba[0]), float.Parse(rgba[1]), float.Parse(rgba[2]), float.Parse(rgba[3])));
                    string[] d = catalogItem1.CustomData.Split('\"');
                    string hex = d[1];
                    BulletImpactColorDefault.Add(HexToColor.GetColorFromString(hex));
                }
            }
        }

        Application.SetActive(true);
    }

    private void OnFailure(PlayFabError obj)
    {
        Debug.Log("Get Initial Data Failed");
    }
}
