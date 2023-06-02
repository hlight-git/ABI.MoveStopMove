using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class Bullet : GameUnit, IBoostable<AbOnBulletBooster>, IResizable
{
    [SerializeField] protected float baseSpeed = Constant.Weapon.DEFAULT_BULLET_BASE_SPEED;
    protected Vector3 destination;
    protected float speed;
    protected bool isActivating;
    protected BulletPath bulletPath;
    public IRanger Attacker { get; private set; }
    public AbOnBulletBooster Booster { get; set; }

    public float Size { get; private set; }

    void Update()
    {
        if (isActivating)
        {
            OnFlying();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isActivating)
        {
            return;
        }
        if (other.CompareTag(Constant.Tag.OBSTACLE))
        {
            OnHitObstacle();
        }
        else if (other.CompareTag(Constant.Tag.CHARACTER))
        {
            IRanger ranger = RangerCache.Get(other);
            if (ranger != Attacker && !ranger.IsDead)
            {
                OnHitSomethingNotObstacle(ranger, OnHitCharacterNoBoost);
            }
        }
    }
    void SpawnBulletPath()
    {
        bulletPath = SimplePool.Spawn<BulletPath>(PoolType.BulletPath, TF.position, TF.rotation);
        bulletPath.SetSize(Size);
        bulletPath.Bullet = this;
    }
    public void SetSize(float size)
    {
        Size = size;
        TF.localScale = size * Vector3.one;
    }
    public virtual void OnInit(IRanger attacker, Vector3 targetPos)
    {
        Attacker = attacker;
        targetPos.y = TF.position.y;
        destination = targetPos;
        TF.LookAt(destination);

        SetSize(attacker.Size);
        speed = attacker.AttackSpeed * baseSpeed;

        SpawnBulletPath();
        isActivating = true;
    }
    protected virtual void OnFlying()
    {
        TF.position = Vector3.MoveTowards(TF.position, destination, speed * Time.deltaTime);
        Booster?.OnExecute();
        if (Vector3.Distance(TF.position, destination) < 0.1f)
        {
            OnReachedDestination();
            Booster?.OnFinish();
        }
    }
    protected virtual void OnReachedDestination()
    {
        OnDespawn();
    }
    protected virtual void OnDespawn()
    {
        SimplePool.Despawn(bulletPath);
        SimplePool.Despawn(this);
        Attacker.Weapon.Reload();
    }
    protected virtual void OnHitObstacle()
    {
        isActivating = false;
        Invoke(nameof(OnDespawn), Constant.Weapon.BULLET_LIFE_TIME_AFTER_HIT_OBSTACLE);
    }
    protected virtual void OnHitSomethingNotObstacle<T>(T hitted, Action<T> onHitAction) where T : IBulletHitable
    {
        if (Booster != null)
        {
            Booster.OnHit(hitted);
        }
        else
        {
            hitted.OnHittedBy(this);
            onHitAction.Invoke(hitted);
        }
    }
    public virtual void OnHitCharacterNoBoost(ICharacter hitChar)
    {
        SimplePool.Spawn<UICombatText>(PoolType.UI_CombatText).OnSpawn(Attacker.TF.position, "+1", ColorData.Ins.Get(Attacker.Color).color);
        ParticlePool.Play(Util.Choice(ParticleType.Hit_1, ParticleType.Hit_2, ParticleType.Hit_3), TF.position);
        OnDespawn();
    }
}