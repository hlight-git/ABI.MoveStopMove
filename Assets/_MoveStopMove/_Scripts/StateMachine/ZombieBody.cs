using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ZombieAnimation
{
    Idle,
    Run,
    Attack,
    FlyDeath,
    StandDeath
}
public class ZombieBody : Body<ZombieAnimation>
{
    protected override void Awake() => base.OnInit(ZombieAnimation.Idle);
    public override void OnDespawn()
    {
    }
}
