using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbStateMachine<T>: GameUnit where T : AbStateMachine<T>
{
    [Header("State Machine:")]
    public string currentStateLog;
    public AbsState<T> CurrentState { get; protected set; }
    private void Awake() => OnInit();
    protected virtual void Update()
    {
        if (IsActivating())
        {
            CurrentState.OnExecute();
        }
    }
    protected virtual void OnInit()
    {
        InitStates();
    }
    public virtual void ChangeState(AbsState<T> state)
    {
        if (CurrentState != state)
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
