using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Data 
public class ShopUIModel : MonoBehaviour
{
    [SerializeField] private GameObject shopUI, errorUI;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private Transform shopItemContanier;
    private Dictionary<string, GameObject> itemId_shopItem;

    [SerializeField] private TMP_Text virtualCurrency;

    public GameObject ShopUI { get => shopUI; set => shopUI = value; }
    public GameObject ErrorUI { get => errorUI; set => errorUI = value; }
    public GameObject ShopItemPrefab { get => shopItemPrefab; set => shopItemPrefab = value; }
    public Transform ShopItemContanier { get => shopItemContanier; set => shopItemContanier = value; }
    public TMP_Text VirtualCurrency { get => virtualCurrency; set => virtualCurrency = value; }
    public Dictionary<string, GameObject> ItemId_shopItem { get => itemId_shopItem; set => itemId_shopItem = value; }

    private void Awake()
    {
        itemId_shopItem = new Dictionary<string, GameObject>();
    }
}
