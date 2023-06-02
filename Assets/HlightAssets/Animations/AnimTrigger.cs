using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AbsAnimTrigger<T>
{
    [SerializeField] Animator animator;
    public T CurrentAnim { get; private set; }
    protected abstract string CurrentAnimTrigger { get; }
    public AbsAnimTrigger() { }
    public AbsAnimTrigger(Animator animator, T defaultAnim)
    {
        this.animator = animator;
        CurrentAnim = defaultAnim;
        this.animator.SetTrigger(CurrentAnimTrigger);
    }
    public void SetAnimator(Animator animator)
    {
        this.animator = animator;
        CurrentAnim = default;
    }
    public void TriggerAnim(T animName)
    {
        if (!animName.Equals(CurrentAnim))
        {
            animator.ResetTrigger(CurrentAnimTrigger); ;
            CurrentAnim = animName;
            animator.SetTrigger(CurrentAnimTrigger);
        }
    }
    public void SetPlaySpeed(float speed)
    {
        animator.speed = speed;
    }
}

[Serializable]
public class AnimTrigger : AbsAnimTrigger<string>
{
    protected override string CurrentAnimTrigger => CurrentAnim;
}

[Serializable]
public class AnimTrigger<T> : AbsAnimTrigger<T> where T : Enum
{
    [SerializeField] AnyStateAnimSet animSet;
    protected override string CurrentAnimTrigger => animSet.Get(CurrentAnim).trigger;
}