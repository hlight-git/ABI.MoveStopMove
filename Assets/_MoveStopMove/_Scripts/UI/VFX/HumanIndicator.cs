using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HumanIndicator : TargetIndicator
{
    [Header("- Human Indicator:")]
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] Image scoreBackground;
    protected override void LateUpdate()
    {
        base.LateUpdate();
        nameTxt.gameObject.SetActive(isVisible);
    }
    public void SetName(string name)
    {
        nameTxt.text = name;
    }
    public void SetScore(string score)
    {
        scoreTxt.text = score;
    }
    public override void SetColor(Color color)
    {
        base.SetColor(color);
        scoreBackground.color = color;
    }
}
