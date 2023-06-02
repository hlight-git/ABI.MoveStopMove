using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : RotateBullet
{
    bool isTurningBack;

    public override void OnInit(IRanger attacker, Vector3 targetPos)
    {
        base.OnInit(attacker, targetPos);
        isTurningBack = false;
    }
    protected override void OnFlying()
    {
        if (isTurningBack && !Attacker.IsDead)
        {
            destination = Attacker.TF.position;
        }
        base.OnFlying();
    }
    protected override void OnReachedDestination()
    {
        if (isTurningBack)
        {
            base.OnReachedDestination();
        }
        else
        {
            TurnBack();
        }
    }
    protected override void OnHitObstacle()
    {
        if (isTurningBack)
        {
            base.OnHitObstacle();
        }
        else
        {
            TurnBack();
        }
    }
    void TurnBack()
    {
        isTurningBack = true;
        destination = Attacker.TF.position;
    }
}
