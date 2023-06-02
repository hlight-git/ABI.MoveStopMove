using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotStopState : RangerStopState<Bot>
{
    public BotStopState(Bot unit) : base(unit)
    {
    }
    protected override ICharacter ChooseTarget()
    {
        unit.ChasingTarget = base.ChooseTarget();
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
            invoker.Schedule(unit.RoamOrChase, Random.value * 3);
        }
    }
}
