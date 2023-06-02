using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public enum State { Buy, Bought, Equipped, Selecting }

    [SerializeField] GameObject[] stateObjects;
    [SerializeField] Color[] colorBg;
    [SerializeField] Image icon;
    [SerializeField] Image bgIcon;

    UIShop shop;

    public int Id { get; private set; }
    public Enum Type { get; private set; }
    public ShopItemData Data { get; private set; }
    public State CurrentState { get; private set; }

    public void SetShop(UIShop shop)
    {
        this.shop = shop;
    }

    public void SetData<T>(int id, ShopItemData<T> itemData, UIShop shop) where T : Enum
    {
        this.shop = shop;
        Id = id;
        Type = itemData.type;
        Data = itemData;
        icon.sprite = itemData.icon;
        bgIcon.color = colorBg[(int)shop.CurrentShopType];
    }

    public void SelectButton()
    {
        shop.SelectItem(this);
    }

    public void SetState(State state)
    {
        for (int i = 0; i < stateObjects.Length; i++)
        {
            stateObjects[i].SetActive(false);
        }

        stateObjects[(int)state].SetActive(true);

        CurrentState = state;
    }

}
