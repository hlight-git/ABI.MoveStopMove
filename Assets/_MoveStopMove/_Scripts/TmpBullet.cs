using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpBullet : GameUnit
{
    [SerializeField] protected float baseSpeed = 6f;
    protected float speed;
    protected AbsCharacter attacker;
    protected bool isRushingForward;

    public virtual void OnInit(AbsCharacter attacker, Vector3 targetPos, float speedCoeff, float size)
    {
        this.attacker = attacker;
        TF.LookAt(targetPos);
        speed = speedCoeff * baseSpeed;
        isRushingForward = true;
    }

    public void OnDespawn()
    {
        SimplePool.Despawn(this);
    }

    protected virtual void OnStop()
    {
        isRushingForward = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.TAG_CHARACTER))
        {
            AbsCharacter hitChar = TmpCache<AbsCharacter>.Get(other);
            if (hitChar != attacker)
            {
                //hitChar.OnHit();
                //attacker.AddScore()
                ParticlePool.Play(Utilities.RandomInMember(ParticleType.Hit_1, ParticleType.Hit_2, ParticleType.Hit_3), TF.position);
                OnDespawn();
            }
        }
        else
        {
            OnStop();
        }

    }
}
