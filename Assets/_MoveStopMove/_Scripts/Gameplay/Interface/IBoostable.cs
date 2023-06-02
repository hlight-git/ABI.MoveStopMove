using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoostable { }

public interface IBoostable<BoosterT> : IBoostable
    where BoosterT : AbBooster
{
    BoosterT Booster { get; set; }
}
