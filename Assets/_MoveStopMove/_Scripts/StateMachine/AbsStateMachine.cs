using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsStateMachine<T>: TmpGameUnit where T : AbsStateMachine<T>
{
    [Header("State Machine:")]
    public string currentStateLog;
    public AbsState<T> CurrentState;
    protected virtual void Update()
    {
        if (IsActivating())
        {
            CurrentState.OnExecute();
        }
    }
    public virtual void OnInit()
    {
        InitStates();
    }
    public void ChangeState(AbsState<T> state)
    {
        if (IsActivating() && CurrentState != state)
        {
            CurrentState?.OnExit();
            CurrentState = state;
            CurrentState.OnEnter();
            currentStateLog = CurrentState.ToString();
        }
    }
    protected abstract bool IsActivating();
    protected abstract void InitStates();
}
