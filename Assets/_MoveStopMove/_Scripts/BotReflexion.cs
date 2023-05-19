using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotReflexion : MonoBehaviour
{
    public TmpBot bot;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.Tag.BULLET))
        {
            Bullet bullet = TmpCache<Bullet>.Get(other);
            if (bullet.Attacker == bot)
            {
                return;
            }
            ReflexToTheAttack(bullet);
        }
    }
    void ReflexToTheAttack(Bullet bullet)
    {
        //bool willDodge = TmpUtil.RandomBool(bot.Proficiency);
        //if (!willDodge)
        //{
        //    return;
        //}
        float dodgeQuality = Random.Range(bot.DodgingLevel, 1);
        Vector3 mainDodgeDir = TmpUtil.RandomSign() * bullet.transform.right;
        Vector3 dodgeDir = Quaternion.AngleAxis(TmpUtil.RandomSign() * 45 * (1 - dodgeQuality), Vector3.up) * mainDodgeDir;
        Vector3 expectedDodgePos = bot.TF.position + Random.Range(2f, 3f) * dodgeDir;
        if (
            NavMesh.SamplePosition(expectedDodgePos, out NavMeshHit hit, float.PositiveInfinity, NavMesh.AllAreas) ||
            NavMesh.SamplePosition(2 * bot.TF.position - expectedDodgePos, out hit, float.PositiveInfinity, NavMesh.AllAreas)
        )
        {
            bot.DodgeTo(hit.position);
        }
        else
        {
            ReflexToTheAttack(bullet);
        }
    }
}
