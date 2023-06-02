using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class RangerStopState<T> : AbsState<T> where T : AbRanger<T>
{
    protected readonly ScheduledInvoker invoker = new ScheduledInvoker();
    protected Vector3 aimingPosition;
    protected bool isBeforeTheThrow;
    protected bool isAttacking;
    protected ICharacter target;

    public RangerStopState(T unit) : base(unit)
    {
    }

    public override void OnEnter()
    {
        unit.ChangeAnim(Constant.Anim.Ranger.IDLE);
    }

    public override void OnExecute()
    {
        if (target != null && (target.IsDead || !unit.IsEnemyInRange(target)))
        {
            unit.RemoveEnemy(target);
            ResetAttack();
            return;
        }
        if (isAttacking)
        {
            if (invoker.Countdown() && isBeforeTheThrow)
            {
                AfterThrow();
            }
            return;
        }
        if (unit.HasEnemyInRange)
        {
            if (unit.Weapon.IsReady)
            {
                target ??= ChooseTarget();
                Aim();
                Attack();
            }
            return;
        }
        OnIdle();
    }
    public override void OnExit()
    {
        unit.ChangeAnimSpeed(1);
        isBeforeTheThrow = false;
        isAttacking = false;
        IgnoreTheTarget();
        invoker.Clear();
    }
    protected virtual ICharacter ChooseTarget()
    {
        return unit.AttackStrategy switch
        {
            RangerAttackStrategy.Nearest => unit.GetNearestEnemy(),
            RangerAttackStrategy.Oldest => unit.GetOldestEnemy(),
            _ => null
        };
    }
    void Attack()
    {
        unit.ChangeAnimSpeed(unit.AttackSpeed);
        unit.ChangeAnim(Constant.Anim.Ranger.ATTACK);
        isAttacking = true;
        isBeforeTheThrow = true;
        invoker.Schedule(
            () => unit.Weapon.Throw(unit, aimingPosition),
            Constant.Anim.Ranger.THROW_DELAY_TIME / unit.AttackSpeed
        );
    }
    void AfterThrow()
    {
        isBeforeTheThrow = false;
        invoker.Schedule(
            AfterAttack,
            (Constant.Anim.Ranger.ATTACK_DURATION - Constant.Anim.Ranger.THROW_DELAY_TIME) / unit.AttackSpeed
        );
    }
    protected virtual void ResetAttack()
    {
        IgnoreTheTarget();
        if (isBeforeTheThrow)
        {
            OnExit();
            OnEnter();
        }
    }
    protected virtual void Aim()
    {
        aimingPosition = target.TF.position;
        aimingPosition.y = unit.TF.position.y;
        unit.TF.LookAt(aimingPosition);
        aimingPosition = unit.TF.position + unit.AttackRange * unit.TF.forward;
    }
    protected virtual void IgnoreTheTarget()
    {
        target = null;
    }
    protected virtual void AfterAttack()
    {
        unit.ChangeAnim(Constant.Anim.Ranger.IDLE);
        isAttacking = false;
    }
    protected abstract void OnIdle();
}
