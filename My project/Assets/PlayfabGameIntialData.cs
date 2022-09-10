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
    private List<Color> bulletImpactColor;

 
    public List<Color> BulletImpactColor { get => bulletImpactColor; set => bulletImpactColor = value; }

    private void Awake()
    {
        bulletImpactColor = new List<Color>();
        var request = new GetCatalogItemsRequest { };
        PlayFabClientAPI.GetCatalogItems(request, OnGetCatalogItemsSuccess, OnFailure);
        if(instance == null)
        {
            instance = this;
        }
    }

    private void OnGetCatalogItemsSuccess(GetCatalogItemsResult obj)
    {
        foreach (CatalogItem catalogItem in obj.Catalog)
        {
            if (catalogItem.ItemId == "Bullet Impact Default")
            {
                List<string> bulletImpactDefault = catalogItem.Bundle.BundledItems;
                foreach (string bulletImpact in bulletImpactDefault)
                {
                    foreach (CatalogItem catalogItem1 in obj.Catalog)
                    {
                        if (catalogItem1.ItemId == bulletImpact)
                        {
                            string[] d = catalogItem1.CustomData.Split("\"");
                            string[] rgba = d[3].Split(",");
                            BulletImpactColor.Add(new Color(float.Parse(rgba[0]), float.Parse(rgba[1]), float.Parse(rgba[2]), float.Parse(rgba[3])));
                        }
                    }
                }
                Application.SetActive(true);
                return;
            }
        }
    }

    private void OnFailure(PlayFabError obj)
    {
        Debug.Log("Get Initial Data Failed");
    }
}
