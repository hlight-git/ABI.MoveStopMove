using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bullet : GameUnit
{
    [SerializeField] protected float baseSpeed = 10;// GameConstant.Weapon.DEFAULT_BULLET_BASE_SPEED;
    protected Vector3 destination;
    protected float speed;
    protected bool isActivating;
    protected BulletPath bulletPath;
    public AbsCharacter Attacker { get; private set; }
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
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            OnHitCharacter(TmpCache<AbsCharacter>.Get(other));
        }
        if (other.CompareTag(GameConstant.Tag.OBSTACLE))
        {
            OnHitObstacle();
        }
    }
    public virtual void OnInit(AbsCharacter attacker, Vector3 targetPos)
    {
        Attacker = attacker;
        targetPos.y = TF.position.y;
        TF.LookAt(targetPos);
        destination = TF.position + attacker.AttackRange * TF.forward;

        TF.localScale = attacker.Size * Vector3.one;
        speed = attacker.AttackSpeed * baseSpeed;
        isActivating = true;

        bulletPath = SimplePool.Spawn<BulletPath>(PoolType.BulletPath, TF.position, TF.rotation);
        bulletPath.Bullet = this;
    }
    protected virtual void OnFlying()
    {
        TF.position = Vector3.MoveTowards(TF.position, destination, speed * Time.deltaTime);
        if (Vector3.Distance(TF.position, destination) < 0.1f)
        {
            OnReachedDestination();
        }
    }
    protected virtual void OnHitCharacter(AbsCharacter hitChar)
    {
        if (hitChar == Attacker || hitChar.IsDead)
        {
            return;
        }
        hitChar.OnHit(TF.forward);
        Attacker.AddScore(hitChar.Score);
        ParticlePool.Play(Utilities.RandomInMember(ParticleType.Hit_1, ParticleType.Hit_2, ParticleType.Hit_3), TF.position);
        OnDespawn();
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
        Invoke(nameof(OnDespawn), GameConstant.Weapon.BULLET_LIFE_TIME_AFTER_HIT_OBSTACLE);
    }


    //protected Character character;
    //[SerializeField] protected float moveSpeed = 6f;
    //protected bool isRunning;

    //public virtual void OnInit(Character character, Vector3 target, float size)
    //{
    //    this.character = character;
    //    TF.forward = (target - TF.position).normalized;
    //    isRunning = true;
    //}

    //public void OnDespawn()
    //{
    //    SimplePool.Despawn(this);
    //}

    //protected virtual void OnStop() { }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag(Constant.TAG_CHARACTER))
    //    {
    //        IHit hit = Cache.GetIHit(other);

    //        if (hit != null && hit != (IHit)character)
    //        {
    //            hit.OnHit(
    //                () =>
    //                {
    //                    character.AddScore(1);
    //                    ParticlePool.Play(Utilities.RandomInMember(ParticleType.Hit_1, ParticleType.Hit_2, ParticleType.Hit_3), TF.position);
    //                    SimplePool.Despawn(this);
    //                });
    //        }
    //    }

    //    if (other.CompareTag(Constant.TAG_BLOCK))
    //    {
    //        OnStop();
    //    }

    //}
}