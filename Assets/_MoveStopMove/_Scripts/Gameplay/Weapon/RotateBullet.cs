using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBullet : Bullet
{
    [SerializeField] protected float rotateSpeed = Constant.Weapon.DEFAULT_BULLET_ROTATION_SPEED;
    [SerializeField] RotationAxis rotationAxis;
    [SerializeField] protected Transform child;
    Vector3 rotateAxisVector;
    public override void OnInit(IRanger attacker, Vector3 targetPos)
    {
        base.OnInit(attacker, targetPos);
        rotateAxisVector = rotationAxis switch
        {
            RotationAxis.Right => Vector3.right,
            RotationAxis.Up => Vector3.up,
            RotationAxis.Forward => Vector3.forward,
            RotationAxis.Back => Vector3.back,
            RotationAxis.Down => Vector3.down,
            RotationAxis.Left => Vector3.left,
            _ => Vector3.zero
        };
    }
    protected override void OnFlying()
    {
        base.OnFlying();
        child.Rotate(rotateAxisVector * rotateSpeed, Space.Self);
    }
    public enum RotationAxis
    {
        Forward, Up, Right, Back, Down, Left
    }
}
