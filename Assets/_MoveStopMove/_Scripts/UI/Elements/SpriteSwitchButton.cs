using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitchButton : AbsSwitchButton<Sprite>
{
    public Image image;
    public override void UpdateState()
    {
        image.sprite = IsTurnOn ? OnAsset : OffAsset;
    }
}
