using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMainMenu : UICanvas
{
    const string MENU_ANIM_TRIGGER = "Menu";
    const string SETTING_ANIM_TRIGGER = "Setting";

    [SerializeField] CanvasGroup group;
    [SerializeField] TextMeshProUGUI playerCoinTxt;
    [SerializeField] TMP_InputField nameInputField;
    [SerializeField] RectTransform coinPoint;
    [SerializeField] Animator animator;
    [SerializeField] SpriteSwitchButton soundButton;
    [SerializeField] SpriteSwitchButton vibrateButton;
    [SerializeField] SpriteSwitchButton adsButton;
    public RectTransform CoinPoint => coinPoint;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.MainMenu);
        CameraFollower.Ins.ChangeState(CameraFollower.State.MainMenu);
        playerCoinTxt.SetText(UserData.Ins.Coin.ToString());
        nameInputField.text = UserData.Ins.Name;
        soundButton.Setup(UserData.Ins.SoundIsOn);
        vibrateButton.Setup(UserData.Ins.Vibrate);
        adsButton.Setup(UserData.Ins.RemoveAds);
        Show();
        ChangeVisible();
    }


    public void ChangeVisibleOfSettingButtons()
    {
        animator.SetTrigger(SETTING_ANIM_TRIGGER);
    }

    public void SoundButton()
    {
        bool tmp = false;
        soundButton.Switch();
        UserData.Ins.SetBoolData(UserData.KEY_SOUNDISON, ref tmp, soundButton.IsTurnOn);
    }

    public void VibrateButton()
    {
        bool tmp = false;
        vibrateButton.Switch();
        UserData.Ins.SetBoolData(UserData.KEY_VIBRATE, ref tmp, vibrateButton.IsTurnOn);
    }

    public void RemoveAdsButton()
    {
        bool tmp = false;
        adsButton.Switch();
        UserData.Ins.SetBoolData(UserData.KEY_REMOVEADS, ref tmp, adsButton.IsTurnOn);
    }

    public void ShopButton()
    {
        UIManager.Ins.OpenUI<UIShop>();
        CloseDirectly();
    }


    public void WeaponButton()
    {
        UIManager.Ins.OpenUI<UIWeapon>();
        CloseDirectly();
    }

    public void PlayButton()
    {
        ChangeVisible();
        Invoke(nameof(Hide), 0.5f);
        Close(1f);
        UserData.Ins.SetStringData(UserData.KEY_PLAYER_NAME, ref UserData.Ins.Name, nameInputField.text);
        LevelManager.Ins.OnStartNormalMode();
        CameraFollower.Ins.ChangeState(CameraFollower.State.Gameplay);
        UIManager.Ins.OpenUI<UIGameplay>();
    }
    public void ChangeVisible()
    {
        animator.SetTrigger(MENU_ANIM_TRIGGER);
    }
    void Hide()
    {
        group.alpha = 0;
    }
    void Show()
    {
        group.alpha = 1;
    }
}