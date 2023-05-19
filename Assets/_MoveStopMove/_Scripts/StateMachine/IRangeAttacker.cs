using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRangeAttacker : IChar
{
    void AddScore(int score);
    void SetAttackSpeed(float attackSpeed);
    void SetAttackRange(float attackRange);
    void AddEnemy(IChar target);
    void RemoveEnemy(IChar target);
    IChar GetNearestEnemy();
    IChar GetOldestEnemy();
    bool IsEnemyInRange(IChar enemy);
    void OnMeetAnEnemy(IChar enemy);
    void OnAnEnemyOutOfRange(IChar enemy);
}
