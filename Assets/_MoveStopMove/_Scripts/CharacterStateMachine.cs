using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterStateMachine<T> : AbsCharacter where T : CharacterStateMachine<T>
{
    public AbsState<T> CurrentState;
    public string state;
    protected virtual void Update()
    {
        if (IsDead)
        {
            return;
        }
        CurrentState.OnExecute();
    }
    public override void OnInit()
    {
        base.OnInit();
        InitStates();
    }
    public void ChangeState(AbsState<T> state)
    {
        if (IsDead)
        {
            return;
        }
        if (CurrentState != state)
        {
            CurrentState?.OnExit();
            CurrentState = state;
            this.state = CurrentState.ToString();
            CurrentState.OnEnter();
        }
    }
    protected abstract void InitStates();
}
