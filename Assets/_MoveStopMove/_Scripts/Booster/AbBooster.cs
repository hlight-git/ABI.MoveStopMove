using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbBooster
{
    public BoosterT Clone<BoosterT>() where BoosterT : AbBooster => MemberwiseClone() as BoosterT;
    public abstract void OnTriggered();
    public abstract void OnExecute();
    public abstract void OnCancel();
    public abstract void OnFinish();
}

public abstract class AbBooster<CollectorT> : AbBooster
{
    public abstract void OnInit(CollectorT collector);
}

public abstract class AbBooster<BoosterT, CollectorT, BoostTargetT> : AbBooster<CollectorT>
    where BoosterT : AbBooster<BoosterT, CollectorT, BoostTargetT>
    where BoostTargetT : IBoostable<BoosterT>
{
    protected CollectorT collector;
    protected abstract BoostTargetT Target { get; }
    public override void OnFinish()
    {
        OnCancel();
        Target.Booster = null;
    }
    public override void OnInit(CollectorT collector)
    {
        this.collector = collector;
        Target.Booster?.OnCancel();
        Target.Booster = this as BoosterT;
        OnTriggered();
    }
}
public abstract class AbOnRangerBooster : AbBooster<AbOnRangerBooster, IRanger, IRanger>
{
    protected override IRanger Target => collector;
}
public abstract class AbOnWeaponBooster : AbBooster<AbOnWeaponBooster, IRanger, Weapon>
{
    protected override Weapon Target => collector.Weapon;
    public AbBoosterData<Bullet> BulletBoosterData { get; set; }
    public virtual void OnThrow(Bullet bullet)
    {
        if (BulletBoosterData != null)
        {
            BulletBoosterData.Boost(bullet);
        }
    }
}
public abstract class AbOnBulletBooster : AbBooster<AbOnBulletBooster, Bullet, Bullet>
{
    protected override Bullet Target => collector;
    public virtual void OnHit(IBulletHitable hitted) => hitted.OnHittedBy(Target);
}