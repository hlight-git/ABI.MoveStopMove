using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotPositionPredictor : CharacterAttackRange
{
    TmpBot bot;

    public Transform TF { get; private set; }
    void Awake()
    {
        TF = transform;
        bot = character as TmpBot;
    }
    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag(GameConstant.Tag.BULLET))
        {
            
        }
    }
    public void OnBulletComing(Transform bulletTF, Vector3 predictedCollisionPoint)
    {
        bool willDodge = TmpUtil.RandomBool(bot.DodgingLevel);
        if (!willDodge)
        {
            return;
        }

        float dodgeQuality = Random.Range(bot.DodgingLevel, 1);
        Vector3 dodgeDir = bulletTF.right * (((TF.position - predictedCollisionPoint).x > 0) ? 1 : -1);
        dodgeDir = Quaternion.AngleAxis(TmpUtil.RandomSign() * 45 * (1 - dodgeQuality), Vector3.up) * dodgeDir;

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
            OnBulletComing(bulletTF, predictedCollisionPoint);
        }
    }
}
