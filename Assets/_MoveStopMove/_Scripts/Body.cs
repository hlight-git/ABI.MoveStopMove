using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Body<AT> : TmpGameUnit
{
    [SerializeField] Animator animator;
    public AnimPlayer<AT> AnimPlayer { get; private set; }
    public virtual void OnInit(AT defaultAnim)
    {
        AnimPlayer = new AnimPlayer<AT>(animator, defaultAnim);
    }
    protected abstract void Awake();
    public abstract void OnDespawn();
}