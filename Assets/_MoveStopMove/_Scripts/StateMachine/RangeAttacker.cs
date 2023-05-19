using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttacker<T> : AbsChar<T>, IRangeAttacker where T : RangeAttacker<T>
{
    [SerializeField] protected Transform sightTF;
    protected List<IChar> enemiesInRange = new List<IChar>();
    protected HumanBody body;
    public CharacterAttackStrategy AttackStrategy;

    public int Score { get; protected set; }
    public float AttackSpeed { get; protected set; }
    public float AttackRange { get; protected set; }

    public void AddEnemy(IChar target)
    {
        enemiesInRange.Add(target);
    }

    public void RemoveEnemy(IChar target)
    {
        if (IsEnemyInRange(target))
        {
            enemiesInRange.Remove(target);
        }
    }

    public void AddScore(int score)
    {
        Score += score;
        SetSize(GameConstant.Character.DEFAULT_SIZE + GameConstant.Character.SIZE_UP_PER_SCORE * Score);
        SetAttackRange(AttackRange + Score);
    }

    public IChar GetNearestEnemy()
    {
        IChar target = enemiesInRange[GameConstant.Collection.FIRST_ELEM_INDEX];
        float minDistance = Vector3.Distance(target.UnitTF.position, UnitTF.position);
        for (int i = 1; i < enemiesInRange.Count; i++)
        {
            float distance = Vector3.Distance(enemiesInRange[i].UnitTF.position, UnitTF.position);
            if (distance < minDistance)
            {
                target = enemiesInRange[i];
                minDistance = distance;
            }
        }
        return target;
    }

    public IChar GetOldestEnemy()
    {
        return enemiesInRange[GameConstant.Collection.FIRST_ELEM_INDEX];
    }

    public bool IsEnemyInRange(IChar enemy)
    {
        return enemiesInRange.Contains(enemy);
    }

    public void OnAnEnemyOutOfRange(IChar enemy)
    {
        RemoveEnemy(enemy);
    }

    public void OnMeetAnEnemy(IChar enemy)
    {
        AddEnemy(enemy);
    }

    public void SetAttackRange(float attackRange)
    {
        AttackRange = attackRange;
        sightTF.localScale = attackRange * Vector3.one;
    }

    public void SetAttackSpeed(float attackSpeed)
    {
        AttackSpeed = Mathf.Clamp(attackSpeed, GameConstant.Character.MIN_ATTACK_SPEED, GameConstant.Character.MAX_ATTACK_SPEED);
    }
}
