using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class AbCharacter<TSelf, TBody, TEnemy, TIndicator> : AbStateMachine<TSelf>, ICharacter
    where TSelf : AbCharacter<TSelf, TBody, TEnemy, TIndicator>
    where TBody : Body
    where TEnemy : ICharacter
    where TIndicator : TargetIndicator
{
    [Header("- Character:")]
    [SerializeField] protected Transform indicatorPoint;
    [SerializeField] AnimTrigger animTrigger;

    protected TIndicator indicator;
    protected TBody body;
    protected SkinType skinType;
    public UnitColor Color { get; protected set; }
    public bool IsDead { get; protected set; }
    public float Size { get; protected set; }
    public int HealthPoint { get; protected set; }
    public UnityAction<ICharacter> OnDeathEvents { get; set; }

    protected override bool IsActivating() => GameManager.Ins.IsPlaying && !IsDead;
    public virtual void OnSpawn()
    {
        InitStats();
    }
    protected virtual void InitStats()
    {
        IsDead = false;
    }
    public virtual void SetSize(float size)
    {
        Size = size;
        TF.localScale = Mathf.Clamp(size, Constant.Ranger.MIN_SIZE, Constant.Ranger.MAX_SIZE) * Vector3.one;
    }

    public virtual void OnDeath()
    {
        IsDead = true;
        OnDeathEvents?.Invoke(this);
    }

    public virtual void OnHittedBy(Bullet bullet)
    {
        HealthPoint -= 1;
        if (HealthPoint <= 0)
        {
            OnDeath();
            bullet.Attacker.AddScore(1);
        }
    }

    public virtual void OnDespawn()
    {
        SimplePool.Despawn(indicator);
    }

    public void ChangeAnim(string animName)
    {
        animTrigger.TriggerAnim(animName);
    }
    public void ChangeAnimSpeed(float speed)
    {
        animTrigger.SetPlaySpeed(speed);
    }
    public virtual void ChangeColor(UnitColor color)
    {
        Color = color;
        indicator.SetColor(ColorData.Ins.Get(Color).color);
    }
    public virtual void SpawnBody()
    {
        body = SimplePool.Spawn<TBody>((PoolType)skinType, TF);
        animTrigger.SetAnimator(body.Animator);
    }
    public void SetIndicatorAlpha(float alpha)
    {
        indicator.SetAlpha(alpha);
    }
}
