using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIVictory : UICanvas
{
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
    }

    public void BonusButton()
    {
        LevelManager.Ins.NextLevel();
        LevelManager.Ins.ReturnMainMenu();

        UIVfx.Ins.AddCoin(coin / Constant.Ranger.COIN_GAIN_PER_SCORE, bonusPoint.position, UIVfx.Ins.CoinPoint);
        UserData.Ins.SetIntData(UserData.KEY_COIN, ref UserData.Ins.Coin, UserData.Ins.Coin + coin);
    }

    public void NextAreaButton()
    {
        LevelManager.Ins.NextLevel();
        LevelManager.Ins.ReturnMainMenu();

        UIVfx.Ins.AddCoin(coin * bonusCoeff / Constant.Ranger.COIN_GAIN_PER_SCORE, mainMenuPoint.position, UIVfx.Ins.CoinPoint);
        UserData.Ins.SetIntData(UserData.KEY_COIN, ref UserData.Ins.Coin, UserData.Ins.Coin + coin);
    }

    internal void SetCoin(int coin)
    {
        this.coin = coin;
        coinTxt.SetText(coin.ToString());
    }
}
