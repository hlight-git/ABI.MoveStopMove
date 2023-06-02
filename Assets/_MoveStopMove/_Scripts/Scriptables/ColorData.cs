using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorData", menuName = "Scriptable Objects/Color Data")]
public class ColorData : SingletonResourceCollection<ColorData, UnitColor, Material>
{
}

public enum UnitColor
{
    Black = 0,
    Blue = 1,
    Brown = 2,
    Cyan = 3,
    Green = 4,
    Orange = 5,
    Pink = 6,
    Red = 7,
    Violet = 8,
    Yellow = 9,

}