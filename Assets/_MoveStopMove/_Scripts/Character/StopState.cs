using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class StopState<T> : AbsState<T> where T : CharacterStateMachine<T>
{
    protected readonly ScheduledInvoker invoker = new ScheduledInvoker();
    protected AbsCharacter target;
    protected Vector3 aimingPosition;
    protected bool isBeforeTheThrow;
    protected bool isAttacking;

    public StopState(T unit) : base(unit)
    {
    }

    public override void OnEnter()
    {
        unit.ChangeAnim(CharacterAnimation.idle);
    }

    public override void OnExecute()
    {
        if (target != null && (target.IsDead || !unit.IsEnemyInRange(target)))
        {
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

        if (unit.HasEnemies)
        {
            if (unit.Weapon.IsReady)
            {
                if (target == null)
                {
                    target = ChooseAttackTarget();
                }
                Aim();
                Attack();
            }
            return;
        }
        OnIdle();
    }
    public override void OnExit()
    {
        isBeforeTheThrow = false;
        isAttacking = false;
        IgnoreTheTarget();
        invoker.Clear();
    }
    protected virtual AbsCharacter ChooseAttackTarget()
    {
        return unit.AttackStrategy switch
        {
            CharacterAttackStrategy.Nearest => unit.GetNearestEnemy(),
            CharacterAttackStrategy.Oldest => unit.GetOldestEnemy(),
            _ => null
        };
    }
    void Attack()
    {
        unit.ChangeAnimSpeed(unit.AttackSpeed);
        unit.ChangeAnim(CharacterAnimation.attack);
        isAttacking = true;
        isBeforeTheThrow = true;
        invoker.Schedule(
            () => unit.Weapon.Throw(unit, aimingPosition),
            GameConstant.Character.THROW_DELAY_TIME / unit.AttackSpeed
        );
    }
    void AfterThrow()
    {
        isBeforeTheThrow = false;
        invoker.Schedule(
            AfterAttack,
            (GameConstant.Character.ATTACK_ANIM_DURATION - GameConstant.Character.THROW_DELAY_TIME) / unit.AttackSpeed
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
        Vector3 lookPoint = target.TF.position;
        lookPoint.y = unit.TF.position.y;
        unit.TF.LookAt(lookPoint);
        aimingPosition = target.TF.position;
    }
    protected virtual void IgnoreTheTarget()
    {
        target = null;
    }
    protected virtual void AfterAttack()
    {
        unit.ChangeAnim(CharacterAnimation.idle);
        isAttacking = false;
    }
    protected abstract void OnIdle();
}
