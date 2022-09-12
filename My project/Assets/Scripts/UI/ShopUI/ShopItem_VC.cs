using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PlayFab.ClientModels;
using System;
using PlayFab;

public class ShopItem_VC : MonoBehaviour
{
    public static Action BuySucess = delegate { };
    public static Action<string> BuyFailure = delegate { };

    [SerializeField] Image shopItemImage;
    [SerializeField] Image image;
    [SerializeField] TMP_Text cost;
    [SerializeField] Button buyButton;
    private string itemId;


    public Image ShopItemImage { get => shopItemImage; set => shopItemImage = value; }
    public Button BuyButton { get => buyButton; set => buyButton = value; }


    public void initialize(string itemId , string cost, Color color)
    {
        this.itemId = itemId;
        this.cost.text = cost;
        image.color = color;
    }

    public void Buy()
    {
        BuyButton.interactable = false;
        var request = new PurchaseItemRequest()
        {
            ItemId = itemId,
            Price = Int32.Parse(cost.text),
            VirtualCurrency = "VC"
        };
        PlayFabClientAPI.PurchaseItem(request, OnBuySuccess, OnFailure);
    }

    private void OnBuySuccess(PurchaseItemResult obj)
    {
        BuySucess?.Invoke();
        shopItemImage.color = HexToColor.GetColorFromString("BEFF4A");
    }

    private void OnFailure(PlayFabError obj)
    {
        BuyButton.interactable = true;
        BuyFailure.Invoke(obj.ErrorMessage);
    }
}
