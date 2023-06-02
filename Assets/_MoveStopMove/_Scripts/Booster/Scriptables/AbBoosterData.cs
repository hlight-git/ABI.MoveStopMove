using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbBoosterData<CollectorT> : ScriptableObject
{
    [SerializeField] Material material;
    public virtual Material Material { get => material; }
    public abstract void Boost(CollectorT collector);
}

public abstract class AbBoosterData<CollectorT, BoosterT> : AbBoosterData<CollectorT>
    where BoosterT : AbBooster<CollectorT>
{
    [SerializeField] BoosterT prototype;
    protected BoosterT booster;
    public override void Boost(CollectorT collector)
    {
        booster = prototype.Clone<BoosterT>();
        booster.OnInit(collector);
    }
}