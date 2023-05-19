using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpPlayerMoveState : AbsState<TmpPlayer>
{
    public TmpPlayerMoveState(TmpPlayer unit) : base(unit)
    {
    }

    public override void OnEnter()
    {
        unit.ChangeAnim(CharacterAnimation.run);
    }
    public override void OnExecute()
    {
        Moving();
    }
    public override void OnExit()
    {
        unit.Rigidbody.velocity = Vector3.zero;
        unit.ChangeAnimSpeed(1);
    }
    private void Moving()
    {
        unit.TF.forward = Vector3.Lerp(unit.TF.forward, unit.MovingDirection, Time.fixedDeltaTime * unit.TurningSpeed);
        unit.Rigidbody.velocity = unit.TF.forward * unit.CurMoveSpeed;
    }
}
