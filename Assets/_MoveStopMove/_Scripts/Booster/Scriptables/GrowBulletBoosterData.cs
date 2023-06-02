using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GrowBullet", menuName = "Scriptable Objects/Booster/Ranger/Bullet/Grow")]
public class GrowBulletBoosterData : AbBoosterData<Bullet, GrowBulletBooster>
{
}

[System.Serializable]
public class GrowBulletBooster : AbOnBulletBooster
{
    [SerializeField] float resizeSpeed;

    public override void OnHit(IBulletHitable hitted)
    {
        base.OnHit(hitted);
        if (hitted is ICharacter)
        {
            Target.OnHitCharacterNoBoost(hitted as ICharacter);
        }
    }
    public override void OnTriggered()
    {
    }

    public override void OnExecute()
    {
        Target.SetSize(Target.Size + resizeSpeed);
    }

    public override void OnCancel()
    {
    }
}