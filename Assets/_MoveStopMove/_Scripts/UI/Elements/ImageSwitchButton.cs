using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitchButton : AbsSwitchButton<Image>
{
    public override void UpdateState()
    {
        OnAsset.gameObject.SetActive(IsTurnOn);
        OffAsset.gameObject.SetActive(!IsTurnOn);
    }
}
