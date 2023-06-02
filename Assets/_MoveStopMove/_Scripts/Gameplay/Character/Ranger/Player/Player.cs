using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbRanger<Player>, IJoystickControllable
{
    [Header("- Player:")]
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float turningSpeed;
    [SerializeField] ParticleSystem reviveVFX;
    public float CurMoveSpeed { get; private set; }
    public Vector3 MovingDirection { get; private set; }
    public float TurningSpeed => turningSpeed;
    public int Coin => Score * Constant.Ranger.COIN_GAIN_PER_SCORE;
    protected override bool IsActivating() => GameManager.Ins.IsState(GameState.GamePlay) && !IsDead;
    protected override void InitStates()
    {
        MoveState = new PlayerMoveState(this);
        StopState = new PlayerStopState(this);
    }
    public override void OnSpawn()
    {
        base.OnSpawn();
        TF.SetPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 180, 0));
    }
    public override void SetAttackRange(float attackRange)
    {
        base.SetAttackRange(attackRange);
        //float rate = (Size - Constant.Ranger.DEFAULT_SIZE) / (Constant.Ranger.MAX_SIZE - Constant.Ranger.DEFAULT_SIZE);
        //CameraFollower.Ins.SetRateOffset(rate < 0 ? 0 : rate);
        CameraFollower.Ins.SetDistanceToTarget(attackRange * 18f / Constant.Ranger.DEFAULT_ATTACK_RANGE);
    }
    public override void SetSize(float size)
    {
        base.SetSize(size);
    }
    public void OnDrag(Vector3 direction, float moveSpeedCoefficient)
    {
        MovingDirection = direction;
        CurMoveSpeed = moveSpeedCoefficient * maxMoveSpeed;
        ChangeAnimSpeed(moveSpeedCoefficient);
        ChangeState(MoveState);
    }
    public void OnRelease()
    {
        ChangeState(StopState);
    }
    public void CreateBody(SkinType skinType, WeaponType weaponType, HatType hatType, AccessoryType accessoryType, PantType pantType, UnitColor color)
    {
        this.skinType = skinType;
        this.weaponType = weaponType;
        this.hatType = hatType;
        this.accessoryType = accessoryType;
        this.pantType = pantType;
        Color = color;
    }
    public void DespawnBody()
    {
        body.OnDespawn();
    }
    public void TryEquipment(Enum equipType)
    {
        if (equipType is WeaponType weapon)
        {
            body.DespawnWeapon();
            body.ChangeWeapon(weapon);
        }
        else if (equipType is HatType hat)
        {
            body.DespawnHat();
            body.ChangeHat(hat);
        }
        else if (equipType is PantType pant)
        {
            body.ChangePant(pant);
        }
        else if (equipType is AccessoryType accessory)
        {
            body.DespawnAccessory();
            body.ChangeAccessory(accessory);
        }
        else if (equipType is SkinType skin)
        {
            DespawnBody();
            skinType = skin;
            SpawnBody();
            ChangeAnim(Constant.Anim.Ranger.DANCE);
        }
    }
    public void OnRevive()
    {
        enemiesInRange.Clear();
        IsDead = false;
        CurrentState = null;
        ChangeState(StopState);

        reviveVFX.Play();
    }
    public void Cheering()
    {
        ChangeState(StopState);
        ChangeAnim(Constant.Anim.Ranger.WIN);
    }

    public override void SetMoveSpeed(float moveSpeed)
    {
        
    }
}
