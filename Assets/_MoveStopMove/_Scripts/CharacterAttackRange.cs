using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackRange : MonoBehaviour
{
    [SerializeField] protected AbsCharacter character;
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.Tag.CHARACTER))
        {
            AbsCharacter enemy = TmpCache<AbsCharacter>.Get(other);
            if (enemy.IsDead)
            {
                return;
            }
            character.OnMeetAnEnemy(enemy);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConstant.Tag.CHARACTER))
        {
            AbsCharacter enemy = TmpCache<AbsCharacter>.Get(other);
            character.OnAnEnemyOutOfRange(enemy);
        }
    }
}
