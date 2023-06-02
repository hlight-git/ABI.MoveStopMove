using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterCollection<BoostTargetT> : ScriptableObject
{
    [SerializeField] List<AbBoosterData<BoostTargetT>> boosters;

    public AbBoosterData<BoostTargetT> GetRandomBooster() => Util.Choice(boosters);
}