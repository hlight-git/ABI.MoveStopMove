using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BulletPath : GameUnit
{
    public Bullet Bullet;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.Tag.CHARACTER))
        {
            IRanger ranger = RangerCache.Get(other);
            if (ranger is Bot && Vector3.Angle(Bullet.TF.forward, TF.position - Bullet.TF.position) < 90)
            {
                (ranger as Bot).OnBulletComing(this);
            }
        }
    }
    public void SetSize(float size)
    {
        TF.localScale = size * Vector3.one;
    }
}
