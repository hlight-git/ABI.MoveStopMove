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
public class Zombie : AbCharacter<Zombie, Body, Player, TargetIndicator>
{
    protected override void InitStates()
    {
        throw new System.NotImplementedException();
    }
}
