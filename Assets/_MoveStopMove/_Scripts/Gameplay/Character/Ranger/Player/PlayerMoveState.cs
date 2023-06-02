using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : AbsState<Player>
{
    public PlayerMoveState(Player unit) : base(unit)
    {
    }

    public override void OnEnter()
    {
        unit.ChangeAnim(Constant.Anim.Ranger.RUN);
    }
    public override void OnExecute()
    {
        Moving();
    }
    public override void OnExit()
    {
        unit.ChangeAnimSpeed(1);
    }
    private void Moving()
    {
        unit.TF.forward = Vector3.Lerp(unit.TF.forward, unit.MovingDirection, Time.fixedDeltaTime * unit.TurningSpeed);
        unit.TF.Translate(Time.deltaTime * unit.CurMoveSpeed * unit.TF.forward, Space.World);
    }
}
