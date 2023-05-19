using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForward : Bullet
{
    protected override void OnFlying()
    {
        TF.Translate(speed * Time.deltaTime * TF.forward, Space.World);
    }
    protected override void OnReachedDestination()
    {
        OnDespawn();
    }
}
