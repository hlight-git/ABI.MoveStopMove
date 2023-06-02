using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class AbsSwitchButton<AssetT> : MonoBehaviour
{
    public AssetT OnAsset;
    public AssetT OffAsset;
    public bool IsTurnOn { get; private set; }
    public void Switch()
    {
        IsTurnOn = !IsTurnOn;
        UpdateState();
    }
    public void Setup(bool isTurnOn)
    {
        IsTurnOn = isTurnOn;
        UpdateState();
    }
    public abstract void UpdateState();
}