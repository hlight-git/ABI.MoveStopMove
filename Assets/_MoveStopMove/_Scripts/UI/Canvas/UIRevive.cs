using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRevive : UICanvas
{
    [SerializeField] TextMeshProUGUI counterTxt;
    [SerializeField] Image counterCircle;
    private float counter;

    public override void Setup()
    {
        base.Setup();
        GameManager.Ins.ChangeState(GameState.Revive);
        counter = 5;
    }

    private void Update()
    {
        if (counter > 0)
        {
            counter -= Time.deltaTime;
            counterCircle.rectTransform.Rotate(0, 0, - Time.deltaTime * 360);
            counterTxt.SetText(counter.ToString("F0"));

            if (counter <= 0)
            {
                CloseButton();
            }
        }
    }

    public void ReviveButton()
    {
        Close(0);
        LevelManager.Ins.OnRevive();
        UIManager.Ins.OpenUI<UIGameplay>();
    }

    public void CloseButton()
    {
        Close(0);
        LevelManager.Ins.Fail();
    }


}
