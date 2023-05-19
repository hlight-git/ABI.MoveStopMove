using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.AI;

public class BotChaseState : TmpBotMoveState
{
    public BotChaseState(TmpBot unit) : base(unit)
    {
    }
    public override void OnExecute()
    {
        if (unit.ChasingTarget.IsDead)
        {
            unit.RoamOrChase();
            return;
        }
        if (unit.IsEnemyInRange(unit.ChasingTarget))
        {
            unit.ChangeState(unit.StopState);
            return;
        }
        unit.Agent.SetDestination(unit.ChasingTarget.TF.position);
    }
}
public class BotDodgeState : TmpBotMoveState
{
    public Vector3 Destination { get; set; }
    public BotDodgeState(TmpBot unit) : base(unit)
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();
        unit.Agent.SetDestination(Destination);
    }
    public override void OnExecute()
    {
        if (IsReachedDestination())
        {
            if (unit.ChasingTarget)
            {
                unit.ChangeState(unit.ChaseState);
                return;
            }
            unit.ChangeState(unit.RoamState);
        }
    }
}

public class BotRoamState : TmpBotMoveState
{
    public BotRoamState(TmpBot unit) : base(unit)
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();
        unit.Agent.SetDestination(TmpLevelManager.Ins.RandomPoint());
    }
    public override void OnExecute()
    {
        if (IsReachedDestination())
        {
            unit.ChangeState(unit.StopState);
        }
    }
}
public abstract class TmpBotMoveState : AbsState<TmpBot>
{
    public TmpBotMoveState(TmpBot unit) : base(unit)
    {
    }

    public override void OnEnter()
    {
        unit.Agent.enabled = true;
        unit.predictedCollider.position += unit.TF.forward;
        unit.ChangeAnim(CharacterAnimation.run);
    }
    //public override void OnExecute()
    //{
    //    if (IsChasing)
    //    {
    //        if (unit.ChasingTarget.IsDead)
    //        {
    //            bool willChangeTarget = TmpUtil.RandomBool();
    //            if (!willChangeTarget || unit.ChaseARandomTarget())
    //            {
    //                unit.Roam();
    //            }
    //            return;
    //        }
    //        unit.Agent.SetDestination(unit.ChasingTarget.TF.position);
    //    }
    //    if (IsReachedDestination())
    //    {
    //        if (!unit.IsDodging)
    //        {
    //            unit.ChangeState(unit.StopState);
    //        }
    //        unit.IsDodging = false;
    //    }
    //}

    public override void OnExit()
    {
        unit.Agent.enabled = false;
        unit.predictedCollider.position = unit.TF.position;
    }
    protected bool IsReachedDestination()
    {
        Vector3 distance = (unit.TF.position - unit.Agent.destination);
        distance.y = 0;
        return distance.sqrMagnitude < 0.1f;
    }
}
