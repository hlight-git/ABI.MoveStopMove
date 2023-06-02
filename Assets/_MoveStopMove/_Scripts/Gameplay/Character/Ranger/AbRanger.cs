using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RangerAttackStrategy
{
    Nearest,
    Oldest
}

public abstract class AbRanger<SelfT> : AbCharacter<SelfT, RangerBody, ICharacter, HumanIndicator>, IRanger
    where SelfT : AbRanger<SelfT>
{
    [Header("- Ranger:")]
    [SerializeField] protected Transform attackRangeTF;

    protected List<ICharacter> enemiesInRange = new List<ICharacter>();
    protected HatType hatType;
    protected PantType pantType;
    protected WeaponType weaponType;
    protected AccessoryType accessoryType;

    public RangerAttackStrategy AttackStrategy;

    public int Score { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public float AttackRange { get; protected set; }
    public AbsState<SelfT> MoveState { get; protected set; }
    public RangerStopState<SelfT> StopState { get; protected set; }
    public string Name { get; protected set; }
    public Weapon Weapon => body.Weapon;
    public bool HasEnemyInRange => enemiesInRange.Count != 0;
    public float Growth => Constant.Ranger.GROWTH_PER_SCORE * Score;

    public AbOnRangerBooster Booster { get; set; }

    protected override void Update()
    {
        base.Update();
        Booster?.OnExecute();
    }
    public override void OnSpawn()
    {
        indicator = SimplePool.Spawn<HumanIndicator>(PoolType.UI_HumanIndicator);
        indicator.OnSpawn(indicatorPoint);
        indicator.SetAlpha(GameManager.Ins.IsState(GameState.GamePlay) ? 1 : 0);
        base.OnSpawn();
        SpawnBody();
        ChangeState(StopState);
    }
    protected override void InitStats()
    {
        base.InitStats();
        enemiesInRange.Clear();
        Score = 0;
        SetSize(Constant.Ranger.DEFAULT_SIZE);
        SetAttackSpeed(Constant.Ranger.DEFAULT_ATTACK_SPEED);
    }
    public override void SetSize(float size)
    {
        base.SetSize(size);
        SetAttackRange(Size * Constant.Ranger.DEFAULT_ATTACK_RANGE);
    }
    public override void OnDeath()
    {
        ChangeState(StopState);
        base.OnDeath();
        ChangeAnim(Constant.Anim.Ranger.DIE);
    }
    public override void SpawnBody()
    {
        base.SpawnBody();
        body.ChangeWeapon(weaponType);
        body.ChangeHat(hatType);
        body.ChangeAccessory(accessoryType);
        body.ChangePant(pantType);
        ChangeColor(Color);
    }
    public override void ChangeColor(UnitColor color)
    {
        base.ChangeColor(color);
        if (body.IsntBodySet)
        {
            body.ChangeMaterial(ColorData.Ins.Get(color));
        }
    }
    public void AddEnemy(ICharacter target)
    {
        enemiesInRange.Add(target);
    }

    public void RemoveEnemy(ICharacter target)
    {
        if (IsEnemyInRange(target))
        {
            enemiesInRange.Remove(target);
        }
    }

    public ICharacter GetNearestEnemy()
    {
        ICharacter target = enemiesInRange[Constant.Collection.FIRST_ELEM_INDEX];
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

    public ICharacter GetOldestEnemy()
    {
        return enemiesInRange[Constant.Collection.FIRST_ELEM_INDEX];
    }

    public bool IsEnemyInRange(ICharacter enemy)
    {
        return enemiesInRange.Contains(enemy);
    }

    public virtual void OnAnEnemyGetInRange(ICharacter enemy)
    {
        AddEnemy(enemy);
    }

    public virtual void OnAnEnemyOutOfRange(ICharacter enemy)
    {
        RemoveEnemy(enemy);
    }

    public void AddScore(int score)
    {
        //SetScore(Score + score);
        Score += score;
        indicator.SetScore(Score.ToString());
        SetSize(Size + Constant.Ranger.GROWTH_PER_SCORE * score);
    }

    public virtual void SetAttackRange(float attackRange)
    {
        AttackRange = attackRange;
        attackRangeTF.localScale = attackRange / Size  * Vector3.one;
    }

    public virtual void SetAttackSpeed(float attackSpeed)
    {
        AttackSpeed = Mathf.Clamp(attackSpeed, Constant.Ranger.MIN_ATTACK_SPEED, Constant.Ranger.MAX_ATTACK_SPEED);
    }

    public void SetName(string name)
    {
        Name = name;
        indicator.SetName(name);
    }
    public abstract void SetMoveSpeed(float moveSpeed);
}
