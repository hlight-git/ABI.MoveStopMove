using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpWeapon : TmpEquipment
{
    [SerializeField] GameObject concrete;
    [SerializeField] BulletType bulletType;

    public bool IsReady => concrete.activeSelf;

    public void SetActive(bool active)
    {
        concrete.SetActive(active);
    }

    private void OnEnable()
    {
        SetActive(true);
    }

    public void Throw(AbsCharacter character, Vector3 targetPos, float speedCoeff, float size, float reloadTime)
    {
        TmpBullet bullet = SimplePool.Spawn<TmpBullet>((PoolType)bulletType, TF.position, Quaternion.identity);
        bullet.OnInit(character, targetPos, speedCoeff, size);
        bullet.TF.localScale = size * Vector3.one;
        SetActive(false);

        Invoke(nameof(OnEnable), reloadTime);
    }
}
