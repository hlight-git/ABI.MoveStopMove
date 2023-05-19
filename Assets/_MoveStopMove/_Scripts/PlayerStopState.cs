using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStopState : StopState<TmpPlayer>
{
    public PlayerStopState(TmpPlayer unit) : base(unit)
    {
    }
    protected override void Aim()
    {
        (target as TmpBot)?.SetWasAimed(true);
        base.Aim();
    }
    protected override void IgnoreTheTarget()
    {
        (target as TmpBot)?.SetWasAimed(false);
        base.IgnoreTheTarget();
    }
    protected override void OnIdle()
    {

    }
}
