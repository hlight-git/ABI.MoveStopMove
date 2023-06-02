using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerSight : MonoBehaviour
{
    [SerializeField] Collider rangerCollider;
    IRanger ranger;
    private void Awake() => ranger = RangerCache.Get(rangerCollider);
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.Tag.CHARACTER))
        {
            IRanger enemy = RangerCache.Get(other);
            if (enemy.IsDead)
            {
                return;
            }
            ranger.OnAnEnemyGetInRange(enemy);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.Tag.CHARACTER))
        {
            IRanger enemy = RangerCache.Get(other);
            ranger.OnAnEnemyOutOfRange(enemy);
        }
    }
}
