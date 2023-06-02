using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIFail : UICanvas
{
    [SerializeField] TextMeshProUGUI rankTxt;
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] RectTransform bonusPoint;
    [SerializeField] RectTransform mainMenuPoint;
    [SerializeField] TextMeshProUGUI bonusText;
    private int coin;
    private int bonusCoeff;

    public override void Open()
    {
        base.Open();
        GameManager.Ins.ChangeState(GameState.Finish);
        bonusCoeff = Random.Range(2, 5);
        bonusText.text = "GET x" + bonusCoeff.ToString();
        rankTxt.SetText("#" + LevelManager.Ins.Alives.ToString());
    }

    public void BonusButton()
    {
        UIVfx.Ins.AddCoin(coin / Constant.Ranger.COIN_GAIN_PER_SCORE * bonusCoeff, bonusPoint.position, UIVfx.Ins.CoinPoint);
        UserData.Ins.SetIntData(UserData.KEY_COIN, ref UserData.Ins.Coin, UserData.Ins.Coin + coin);
        LevelManager.Ins.ReturnMainMenu();
    }

    public void MainMenuButton()
    {
        UIVfx.Ins.AddCoin(coin / Constant.Ranger.COIN_GAIN_PER_SCORE, mainMenuPoint.position, UIVfx.Ins.CoinPoint);
        UserData.Ins.SetIntData(UserData.KEY_COIN, ref UserData.Ins.Coin, UserData.Ins.Coin + coin);
        LevelManager.Ins.ReturnMainMenu();
    }

    public void SetCoin(int coin)
    {
        this.coin = coin;
        coinTxt.SetText(coin.ToString());
    }
}
