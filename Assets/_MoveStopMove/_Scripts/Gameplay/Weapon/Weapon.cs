using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : GameUnit, IBoostable<AbOnWeaponBooster>, IBoostable
{
    [SerializeField] GameObject render;
    [SerializeField] BulletType bulletType;
    public AbOnWeaponBooster Booster { get; set; }
    public bool IsReady => render.activeSelf;

    void OnEnable() => Reload();
    void Update() => Booster?.OnExecute();
    public void Reload() => render.SetActive(true);
    public void Throw(IRanger character, Vector3 targetPos)
    {
        Bullet bullet = SimplePool.Spawn<Bullet>((PoolType)bulletType, TF.position, Quaternion.identity);
        bullet.OnInit(character, targetPos);
        Booster?.OnThrow(bullet);
        render.SetActive(false);
    }
}
