using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Events;

public enum CharacterAnimation
{
    idle,
    run,
    attack,
    die,
    dance,
    win
}
public enum CharacterAttackStrategy
{
    Nearest,
    Oldest
}

public abstract class AbsCharacter : GameUnit
{
    #region Attributes & Properties
    [Header("Character properties:")]
    [SerializeField] protected Transform sightTF;
    protected List<AbsCharacter> enemiesInRange = new List<AbsCharacter>();
    protected Skin skin;

    public Rigidbody Rigidbody;
    public CharacterAttackStrategy AttackStrategy;

    // Events
    public UnityAction<AbsCharacter> GameStateUpdateOnDeathEvents { get; set; }
    public UnityAction<AbsCharacter> OnDeathEvents { get; set; }

    // Stats
    //public bool IsDead { get; protected set; }
    public bool IsDead;
    public int Score { get; protected set; }
    public float Size { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public float AttackRange { get; protected set; }

    public Weapon Weapon => skin.Weapon;
    public bool HasEnemies => enemiesInRange.Count != 0;
    public bool IsMoving => skin.AnimPlayer.CurrentAnim == CharacterAnimation.run;
    public virtual bool IsValidToUpdate => GameManager.Ins.IsState(GameState.GamePlay) || GameManager.Ins.IsState(GameState.Setting);
    #endregion
    public virtual void OnInit()
    {
        GameStateUpdateOnDeathEvents = null;
        OnDeathEvents = null;
        enemiesInRange.Clear();
        InitStats();
        WearClothes();
        //indicator = SimplePool.Spawn<TargetIndicator>(PoolType.TargetIndicator);
        //indicator.SetTarget(indicatorPoint);
    }
    #region Stats
    protected virtual void InitStats()
    {
        IsDead = false;
        Score = 0;
        SetSize(GameConstant.Character.DEFAULT_SIZE);
        SetAttackSpeed(GameConstant.Character.DEFAULT_ATTACK_SPEED);
        SetAttackRange(GameConstant.Character.DEFAULT_ATTACK_RANGE);
    }
    public virtual void AddScore(int score)
    {
        Score += score;
        SetSize(GameConstant.Character.DEFAULT_SIZE + GameConstant.Character.SIZE_UP_PER_SCORE * Score);
        SetAttackRange(AttackRange + Score);
    }
    protected virtual void SetSize(float size)
    {
        Size = Mathf.Clamp(size, GameConstant.Character.MIN_SIZE, GameConstant.Character.MAX_SIZE);
        TF.localScale = Size * Vector3.one;
    }
    protected virtual void SetAttackSpeed(float attackSpeed)
    {
        AttackSpeed = Mathf.Clamp(attackSpeed, GameConstant.Character.MIN_ATTACK_SPEED, GameConstant.Character.MAX_ATTACK_SPEED);
    }
    protected virtual void SetAttackRange(float attackRange)
    {
        AttackRange = attackRange;
        sightTF.localScale = attackRange * Vector3.one;
    }
    public void OnHit(Vector3 attackDirection)
    {
        if (IsDead)
        {
            return;
        }
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.AddForce(attackDirection * GameConstant.Weapon.BULLET_PUSH_FORCE);
        OnDeath();
    }
    public virtual void OnDeath()
    {
        IsDead = true;
        GameStateUpdateOnDeathEvents?.Invoke(this);
        OnDeathEvents?.Invoke(this);
        ChangeAnim(CharacterAnimation.die);
    }
    public virtual void OnDespawn()
    {
        TakeOffClothes();
        //SimplePool.Despawn(indicator);
    }

    #endregion Stats
    #region Enemies
    public void AddEnemy(AbsCharacter target)
    {
        enemiesInRange.Add(target);
        target.OnDeathEvents += RemoveEnemy;
    }
    public void RemoveEnemy(AbsCharacter target)
    {
        if (!enemiesInRange.Contains(target))
        {
            return;
        }
        enemiesInRange.Remove(target);
        target.OnDeathEvents -= RemoveEnemy;
    }
    public AbsCharacter GetNearestEnemy()
    {
        AbsCharacter target = enemiesInRange[GameConstant.Collection.FIRST_ELEM_INDEX];
        float minDistance = Vector3.Distance(target.TF.position, TF.position);
        for (int i = 1; i < enemiesInRange.Count; i++)
        {
            float distance = Vector3.Distance(enemiesInRange[i].TF.position, TF.position);
            if (distance < minDistance)
            {
                target = enemiesInRange[i];
                minDistance = distance;
            }
        }
        return target;
    }
    public AbsCharacter GetOldestEnemy()
    {
        return enemiesInRange[GameConstant.Collection.FIRST_ELEM_INDEX];
    }
    public bool IsEnemyInRange(AbsCharacter enemy)
    {
        return enemiesInRange.Contains(enemy);
    }
    public virtual void OnMeetAnEnemy(AbsCharacter enemy)
    {
        AddEnemy(enemy);
    }

    public virtual void OnAnEnemyOutOfRange(AbsCharacter enemy)
    {
        RemoveEnemy(enemy);
    }
    #endregion
    #region Animations
    public void ChangeAnim(CharacterAnimation animState)
    {
        skin.AnimPlayer.PlayAnim(animState);
    }
    public void ChangeAnimSpeed(float speed)
    {
        skin.AnimPlayer.SetPlaySpeed(speed);
    }
    #endregion
    #region Skin
    public void ChangeSkin(SkinType skinType)
    {
        skin = SimplePool.Spawn<Skin>((PoolType)skinType, TF);
    }
    public void ChangeWeapon(WeaponType weaponType)
    {
        skin.ChangeWeapon(weaponType);
    }

    public void ChangeAccessory(AccessoryType accessoryType)
    {
        skin.ChangeAccessory(accessoryType);
    }

    public void ChangeHat(HatType hatType)
    {
        skin.ChangeHat(hatType);
    }

    public void ChangePant(PantType pantType)
    {
        skin.ChangePant(pantType);
    }
    public void TakeOffClothes()
    {
        skin.OnDespawn();
        SimplePool.Despawn(skin);
    }
    #endregion
    public abstract void WearClothes();
}
