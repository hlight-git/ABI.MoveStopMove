using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpPlayer : CharacterStateMachine<TmpPlayer>, IJoystickControllable
{
    [Header("Player properties:")]
    [SerializeField] Joystick joystick;

    SkinType skinType;
    WeaponType weaponType;
    HatType hatType;
    AccessoryType accessoryType;
    PantType pantType;

    public float TurningSpeed;
    public float MaxMoveSpeed;

    //SkinType skinType = SkinType.SKIN_Normal;
    //WeaponType weaponType = WeaponType.W_Candy_1;
    //HatType hatType = HatType.HAT_Cap;
    //AccessoryType accessoryType = AccessoryType.ACC_Headphone;
    //PantType pantType = PantType.Pant_1;
    public TmpPlayerMoveState MoveState { get; protected set; }
    public PlayerStopState StopState { get; protected set; }
    public Vector3 MovingDirection { get; private set; }
    public float CurMoveSpeed { get; private set; }

    protected override void InitStates()
    {
        MoveState = new TmpPlayerMoveState(this);
        StopState = new PlayerStopState(this);
        ChangeState(StopState);
    }
    public override void OnInit()
    {
        OnLoadEquiptmentsData();
        base.OnInit();

        joystick.SetTarget(this);
        //indicator.SetName("YOU");
    }
    public override void WearClothes()
    {
        ChangeSkin(skinType);
        ChangeWeapon(weaponType);
        ChangeHat(hatType);
        ChangeAccessory(accessoryType);
        ChangePant(pantType);
    }

    public void OnDrag(Vector3 direction, float moveSpeedCoefficient)
    {
        MovingDirection = direction;
        CurMoveSpeed = moveSpeedCoefficient * MaxMoveSpeed;
        ChangeAnimSpeed(moveSpeedCoefficient);
        ChangeState(MoveState);
    }
    public void OnRelease()
    {
        ChangeState(StopState);
    }
    void OnLoadEquiptmentsData()
    {
        skinType = UserData.Ins.playerSkin;
        weaponType = UserData.Ins.playerWeapon;
        hatType = UserData.Ins.playerHat;
        accessoryType = UserData.Ins.playerAccessory;
        pantType = UserData.Ins.playerPant;
    }
    public override void OnAnEnemyOutOfRange(AbsCharacter enemy)
    {
        base.OnAnEnemyOutOfRange(enemy);
    }
}
