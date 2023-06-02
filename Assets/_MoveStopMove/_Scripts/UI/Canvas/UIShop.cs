using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : UICanvas
{
    public enum ShopType { Hat, Pant, Accessory, Skin }

    [SerializeField] ShopData data;
    [SerializeField] ShopItem itemPrefab;
    [SerializeField] Transform contentFieldTF;
    [SerializeField] ShopFragment[] shopFragments;

    [SerializeField] TextMeshProUGUI playerCoinTxt;

    [SerializeField] ButtonState buttonState;
    [SerializeField] Text coinTxt;
    [SerializeField] Text adsTxt;

    MiniPool<ShopItem> miniPool = new MiniPool<ShopItem>();

    ShopFragment currentFragment;
    ShopItem itemEquiped;
    ShopItem currentItem;

    public ShopType CurrentShopType => currentFragment.Type;

    private void Awake()
    {
        miniPool.OnInit(itemPrefab, 10, contentFieldTF);

        for (int i = 0; i < shopFragments.Length; i++)
        {
            shopFragments[i].SetShop(this);
        }
    }

    public override void Open()
    {
        base.Open();
        SelectFragment(shopFragments[0]);
        CameraFollower.Ins.ChangeState(CameraFollower.State.Shop);
        LevelManager.Ins.Player.ChangeAnim(Constant.Anim.Ranger.DANCE);

        playerCoinTxt.SetText(UserData.Ins.Coin.ToString());
    }

    public override void CloseDirectly()
    {
        base.CloseDirectly();
        UIManager.Ins.OpenUI<UIMainMenu>();

        LevelManager.Ins.Player.DespawnBody();
        GameManager.Ins.InjectUserData();
        LevelManager.Ins.Player.SpawnBody();
        LevelManager.Ins.Player.ChangeAnim(Constant.Anim.Ranger.IDLE);
    }

    internal void SelectFragment(ShopFragment shopFragment)
    {
        if (currentFragment != null)
        {
            currentFragment.Active(false);
        }

        currentFragment = shopFragment;
        currentFragment.Active(true);

        miniPool.Collect();
        itemEquiped = null;

        switch (currentFragment.Type)
        {
            case ShopType.Hat:
                InitShipItems(data.Hats.Items, ref itemEquiped);
                break;
            case ShopType.Pant:
                InitShipItems(data.Pants.Items, ref itemEquiped);
                break;
            case ShopType.Accessory:
                InitShipItems(data.Accessories.Items, ref itemEquiped);
                break;
            case ShopType.Skin:
                InitShipItems(data.Skins.Items, ref itemEquiped);
                break;
            default:
                break;
        }

    }

    private void InitShipItems<T>(List<ShopItemData<T>> items, ref ShopItem itemEquiped)  where T : Enum
    {
        for (int i = 0; i < items.Count; i++)
        {
            ShopItem.State state = UserData.Ins.GetEnumData(items[i].type.ToString(), ShopItem.State.Buy);
            ShopItem item = miniPool.Spawn();
            item.SetData(i, items[i], this);
            item.SetState(state);
            
            if (state == ShopItem.State.Equipped)
            {
                SelectItem(item);
                itemEquiped = item;
            }

        }
    }

    public ShopItem.State GetState(Enum t) => UserData.Ins.GetEnumData(t.ToString(), ShopItem.State.Buy);

    internal void SelectItem(ShopItem item)
    {
        if (currentItem != null)
        {
            currentItem.SetState(GetState(currentItem.Type));
        }

        currentItem = item;
        //if (currentItem.Data.cost == 0)
        //{
        //    if (
        //        (CurrentShopType == ShopType.Hat && (HatType)currentItem.Type == TmpUserData.Ins.Hat) ||
        //        (CurrentShopType == ShopType.Pant && (PantType)currentItem.Type == TmpUserData.Ins.Pant) ||
        //        (CurrentShopType == ShopType.Accessory && (AccessoryType)currentItem.Type == TmpUserData.Ins.Accessory) ||
        //        (CurrentShopType == ShopType.Skin && (SkinType)currentItem.Type == TmpUserData.Ins.Skin)
        //    )
        //    {
        //        buttonState.SetState(ButtonState.State.Equiped);
        //    }
        //    else
        //    {
        //        buttonState.SetState(ButtonState.State.Equip);
        //    }
        //}
        switch (currentItem.CurrentState)
        {
            case ShopItem.State.Buy:
                buttonState.SetState(ButtonState.State.Buy);
                break;
            case ShopItem.State.Bought:
                buttonState.SetState(ButtonState.State.Equip);
                break;
            case ShopItem.State.Equipped:
                buttonState.SetState(ButtonState.State.Equiped);
                break;
            default:
                break;
        }

        LevelManager.Ins.Player.TryEquipment(currentItem.Type);
        currentItem.SetState(ShopItem.State.Selecting);

        //check data
        coinTxt.text = item.Data.cost.ToString();
        adsTxt.text = item.Data.ads.ToString();
    }

    public void BuyButton()
    {
        //TODO: check xem du tien hay k

        UserData.Ins.SetEnumData(currentItem.Type.ToString(), ShopItem.State.Bought);
        SelectItem(currentItem);
    }

    public void AdsButton()
    {

    }

    public void EquipButton()
    {
        if (currentItem != null)
        {
            UserData.Ins.SetEnumData(currentItem.Type.ToString(), ShopItem.State.Equipped);

            switch (CurrentShopType)
            {
                case ShopType.Hat:
                    //reset trang thai do dang deo ve bought
                    UserData.Ins.SetEnumData(UserData.Ins.Hat.ToString(), ShopItem.State.Bought);
                    //save id do moi vao player
                    UserData.Ins.SetEnumData(UserData.KEY_PLAYER_HAT, ref UserData.Ins.Hat, (HatType)currentItem.Type);
                    break;
                case ShopType.Pant:
                    UserData.Ins.SetEnumData(UserData.Ins.Pant.ToString(), ShopItem.State.Bought);
                    UserData.Ins.SetEnumData(UserData.KEY_PLAYER_PANT, ref UserData.Ins.Pant, (PantType)currentItem.Type);
                    break;
                case ShopType.Accessory:
                    UserData.Ins.SetEnumData(UserData.Ins.Accessory.ToString(), ShopItem.State.Bought);
                    UserData.Ins.SetEnumData(UserData.KEY_PLAYER_ACCESSORY, ref UserData.Ins.Accessory, (AccessoryType)currentItem.Type);
                    break;
                case ShopType.Skin:
                    UserData.Ins.SetEnumData(UserData.Ins.Skin.ToString(), ShopItem.State.Bought);
                    UserData.Ins.SetEnumData(UserData.KEY_PLAYER_SKIN, ref UserData.Ins.Skin, (SkinType)currentItem.Type);
                    break;
                default:
                    break;
            }
  
        }

        if (itemEquiped != null)
        {
            itemEquiped.SetState(ShopItem.State.Bought);
        }

        currentItem.SetState(ShopItem.State.Equipped);
        SelectItem(currentItem);
    }

}
