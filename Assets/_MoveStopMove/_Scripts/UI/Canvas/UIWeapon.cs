using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIWeapon : UICanvas
{
    public Transform weaponPoint;

    [SerializeField] ButtonState buttonState;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI playerCoinTxt;
    [SerializeField] Text costTxt;
    [SerializeField] Text adsTxt;

    private Weapon currentWeapon;
    private WeaponType weaponType;

    public override void Setup()
    {
        base.Setup();
        ShowWeapon(UserData.Ins.Weapon);
        playerCoinTxt.SetText(UserData.Ins.Coin.ToString());
    }

    public override void CloseDirectly()
    {
        base.CloseDirectly();

        if (currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
            currentWeapon = null;
        }

        UIManager.Ins.OpenUI<UIMainMenu>();
    }

    public void NextButton()
    {
        ShowWeapon(WeaponData.Ins.NextItem(weaponType));
    }

    public void PrevButton()
    {
        ShowWeapon(WeaponData.Ins.PrevItem(weaponType));
    }

    public void BuyButton()
    {
        if (UserData.Ins.Coin > WeaponData.Ins.Get(weaponType).cost)
        {
            UserData.Ins.SetIntData(UserData.KEY_COIN, ref UserData.Ins.Coin, UserData.Ins.Coin - WeaponData.Ins.Get(weaponType).cost);
            UserData.Ins.SetEnumData(weaponType.ToString(), ShopItem.State.Bought);
            playerCoinTxt.SetText(UserData.Ins.Coin.ToString());
            ShowWeapon(weaponType);
        }
    }

    public void AdsButton()
    {
        int ads = UserData.Ins.GetDataState(weaponType + "Ads");
        UserData.Ins.SetDataState(weaponType + "Ads", ads + 1);

        if (ads + 1 >= WeaponData.Ins.Get(weaponType).ads)
        {
            UserData.Ins.SetDataState(weaponType.ToString(), 1);
            ShowWeapon(weaponType);
        }
    }

    public void EquipButton()
    {
        UserData.Ins.SetEnumData(weaponType.ToString(), ShopItem.State.Equipped);
        UserData.Ins.SetEnumData(UserData.Ins.Weapon.ToString(), ShopItem.State.Bought);
        UserData.Ins.SetEnumData(UserData.KEY_PLAYER_WEAPON, ref UserData.Ins.Weapon, weaponType);
        ShowWeapon(weaponType);
        LevelManager.Ins.Player.TryEquipment(weaponType);
    }

    public void ShowWeapon(WeaponType weaponType)
    {
        this.weaponType = weaponType;

        if (currentWeapon != null)
        {
            SimplePool.Despawn(currentWeapon);
        }
        currentWeapon = SimplePool.Spawn<Weapon>((PoolType)weaponType, Vector3.zero, Quaternion.identity, weaponPoint);

        //check data dong
        ButtonState.State state = (ButtonState.State)UserData.Ins.GetDataState(weaponType.ToString(), 0);
        buttonState.SetState(state);

        TmpWeaponItem item = WeaponData.Ins.Get(weaponType);
        nameTxt.SetText(item.name);
        costTxt.text = item.cost.ToString();
        adsTxt.text = item.ads.ToString();
    }

}
