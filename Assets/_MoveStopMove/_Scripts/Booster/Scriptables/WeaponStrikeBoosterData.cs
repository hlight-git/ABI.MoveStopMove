using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStrike", menuName = "Scriptable Objects/Booster/Ranger/Weapon/WeaponStrike")]
public class WeaponStrikeBoosterData : AbBoosterData<IRanger, WeaponStrikeBooster>
{
    [SerializeField] AbBoosterData<Bullet> bulletBoosterData;
    public override Material Material => bulletBoosterData.Material;
    public override void Boost(IRanger collector)
    {
        base.Boost(collector);
        booster.BulletBoosterData = bulletBoosterData;
    }
}

[System.Serializable]
public class WeaponStrikeBooster : AbOnWeaponBooster
{
    float attackRangeBoostValue;
    float attackRangeBackUp;
    public override void OnTriggered()
    {
        attackRangeBackUp = collector.AttackRange;
        attackRangeBoostValue = attackRangeBackUp + 2;
        collector.SetAttackRange(attackRangeBoostValue);
    }
    public override void OnThrow(Bullet bullet)
    {
        base.OnThrow(bullet);
        OnFinish();
    }
    public override void OnExecute()
    {
    }

    public override void OnCancel()
    {
        collector.SetAttackRange(attackRangeBackUp);
    }
}