using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    const string OPEN_CLOSE_ANIM = "Close";
    const string OPEN_SETTING_ANIM = "Setting";

    [SerializeField] TextMeshProUGUI playerCoinTxt;
    [SerializeField] RectTransform coinPoint;
    [SerializeField] Animator animator;
    public RectTransform CoinPoint => coinPoint;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        CameraFollower.Ins.ChangeState(CameraFollower.State.MainMenu);

        playerCoinTxt.SetText(UserData.Ins.coin.ToString());
    }


    public void SettingButton()
    {
        animator.SetTrigger(OPEN_SETTING_ANIM);
    }

    public void ShopButton()
    {
        UIManager.Ins.OpenUI<UIShop>();
        Close(0);
    }


    public void WeaponButton()
    {
        UIManager.Ins.OpenUI<UIWeapon>();
        Close(0);
    }

    public void PlayButton()
    {
        LevelManager.Ins.OnPlay();
        UIManager.Ins.OpenUI<UIGameplay>();
        CameraFollower.Ins.ChangeState(CameraFollower.State.Gameplay);

        animator.SetTrigger(OPEN_CLOSE_ANIM);
        Close(0.5f);
    }
}

