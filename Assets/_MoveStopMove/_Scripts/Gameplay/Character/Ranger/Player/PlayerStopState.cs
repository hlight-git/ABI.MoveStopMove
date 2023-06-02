using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopState : RangerStopState<Player>
{
    public PlayerStopState(Player unit) : base(unit)
    {
    }
    protected override void Aim()
    {
        (target as Bot)?.SetWasAimed(true);
        base.Aim();
    }
    protected override void IgnoreTheTarget()
    {
        (target as Bot)?.SetWasAimed(false);
        base.IgnoreTheTarget();
    }
    protected override void OnIdle()
    {

    }
}
