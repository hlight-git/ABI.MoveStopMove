using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PantData", menuName = "Scriptable Objects/Pant Data")]
public class PantData : SingletonResourceCollection<PantData, PantType, Material>
{
}

public enum PantType
{
    Pant_1 = 0,
    Pant_2 = 1,
    Pant_3 = 2,
    Pant_4 = 3,
    Pant_5 = 4,
    Pant_6 = 5,
    Pant_7 = 6,
    Pant_8 = 7,
    Pant_9 = 8,

}