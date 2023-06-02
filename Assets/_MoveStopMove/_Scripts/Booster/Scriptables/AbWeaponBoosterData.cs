//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class AbWeaponBoosterData : AbBoosterData<IRanger, AbOnWeaponBooster>
//{
//    [SerializeField] AbBoosterData<Bullet> bulletBoosterData;
//    public override Material Material => bulletBoosterData.Material;
//    public override void Boost(IRanger collector)
//    {
//        base.Boost(collector);
//        booster.BulletBoosterData = bulletBoosterData;
//    }
//}
