using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotStopState : StopState<TmpBot>
{
    public BotStopState(TmpBot unit) : base(unit)
    {
    }
    protected override AbsCharacter ChooseAttackTarget()
    {
        unit.ChasingTarget = base.ChooseAttackTarget();
        return unit.ChasingTarget;
    }
    protected override void OnIdle()
    {
        if (!unit.IsDead && unit.ChasingTarget != null && !unit.ChasingTarget.IsDead)
        {
            unit.Chase(unit.ChasingTarget);
            return;
        }
        if (invoker.HasScheduledAction)
        {
            invoker.Countdown();
        }
        else
        {
            invoker.Schedule(unit.RoamOrChase, Random.Range(1f, 3f));
        }
    }
}
