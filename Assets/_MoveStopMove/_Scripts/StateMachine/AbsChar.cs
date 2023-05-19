using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public abstract class AbsChar<T> : AbsStateMachine<T>, IChar where T : AbsChar<T>
{
    public Rigidbody Rigidbody;
    public bool IsDead;
    public float Size { get; protected set; }
    protected override bool IsActivating() => IsDead;

    protected override void InitStates()
    {
        throw new System.NotImplementedException();
    }
    protected virtual void InitStats()
    {
        IsDead = false;
        SetSize(GameConstant.Character.DEFAULT_SIZE);
    }
    public virtual void SetSize(float size)
    {
        Size = Mathf.Clamp(size, GameConstant.Character.MIN_SIZE, GameConstant.Character.MAX_SIZE);
        UnitTF.localScale = Size * Vector3.one;
    }
    public virtual void OnDeath()
    {
        IsDead = true;
    }

    public void OnHit()
    {
        throw new System.NotImplementedException();
    }

    //public abstract void OnHit(AbsChar charac);
    //public virtual void OnDespawn()
    //{
    //    TakeOffClothes();
    //    //SimplePool.Despawn(indicator);
    //}
}
