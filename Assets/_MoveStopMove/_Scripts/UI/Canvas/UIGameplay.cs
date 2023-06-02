using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameplay : UICanvas
{
    [SerializeField] Joystick joystick;
    [SerializeField] TextMeshProUGUI characterAmountTxt;
    private void Awake()
    {
        joystick.SetTarget(LevelManager.Ins.Player);
    }
    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.GamePlay);
        UpdateTotalCharacter();
        LevelManager.Ins.SetTargetIndicatorsAlpha(1);
    }

    public override void CloseDirectly()
    {
        base.CloseDirectly();
        LevelManager.Ins.SetTargetIndicatorsAlpha(0);
    }

    public void SettingButton()
    {
        UIManager.Ins.OpenUI<UISetting>();
    }

    public void UpdateTotalCharacter()
    {
        characterAmountTxt.SetText(LevelManager.Ins.Alives.ToString());
    }
}
