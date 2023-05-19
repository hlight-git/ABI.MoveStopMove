using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimPlayer<T>
{
    [SerializeField] Animator animator;
    public T CurrentAnim { get; private set; }
    public AnimPlayer(Animator animator)
    {
        this.animator = animator;
    }
    public AnimPlayer(Animator animator, T defaultAnimState)
    {
        this.animator = animator;
        CurrentAnim = defaultAnimState;
        this.animator.SetTrigger(CurrentAnim.ToString());
    }
    public void PlayAnim(T anim)
    {
        if (!anim.Equals(CurrentAnim))
        {
            animator.ResetTrigger(CurrentAnim.ToString());
            CurrentAnim = anim;
            animator.SetTrigger(CurrentAnim.ToString());
        }
    }
    public void SetPlaySpeed(float speed)
    {
        animator.speed = speed;
    }
}
