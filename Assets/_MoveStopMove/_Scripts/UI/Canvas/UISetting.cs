using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISetting : UICanvas
{
    [SerializeField] ImageSwitchButton soundButton;
    [SerializeField] ImageSwitchButton vibrateButton;
    public override void Setup()
    {
        base.Setup();
        GameManager.Ins.ChangeState(GameState.Setting);
        UIManager.Ins.CloseUI<UIGameplay>();
    }
    public override void Open()
    {
        base.Open();
        soundButton.Setup(UserData.Ins.SoundIsOn);
        vibrateButton.Setup(UserData.Ins.Vibrate);
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

    public void ContinueButton()
    {
        UIManager.Ins.OpenUI<UIGameplay>();
        Close(0);
    }

    public void HomeButton()
    {
        LevelManager.Ins.ReturnMainMenu();
    }

}
