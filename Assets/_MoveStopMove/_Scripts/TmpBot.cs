using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.UI.CanvasScaler;

public class TmpBot : CharacterStateMachine<TmpBot>
{
    [Header("Bot properties:")]
    [SerializeField] protected GameObject reticle;
    public Transform predictedCollider;

    public NavMeshAgent Agent;
    public AbsCharacter ChasingTarget;

    [Range(0f, 1f)]
    public float DodgingLevel;

    public BotRoamState RoamState { get; protected set; }
    public BotDodgeState DodgeState { get; protected set; }
    public BotChaseState ChaseState { get; protected set; }
    public BotStopState StopState { get; protected set; }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameConstant.Tag.BULLET_PATH))
        {
            BulletPath bulletPath = TmpCache<BulletPath>.Get(other);
            if (bulletPath.Bullet.Attacker == this)
            {
                return;
            }
            if (Vector3.Angle(bulletPath.Bullet.TF.forward, TF.position - bulletPath.Bullet.TF.position) > 90)
            {
                return;
            }
            OnBulletComing(bulletPath);
        }
    }
    public override void OnInit()
    {
        Agent.Warp(TF.position);
        base.OnInit();
    }
    protected override void InitStates()
    {
        RoamState = new BotRoamState(this);
        DodgeState = new BotDodgeState(this);
        ChaseState = new BotChaseState(this);
        StopState = new BotStopState(this);
        Roam();
    }
    public override void OnMeetAnEnemy(AbsCharacter enemy)
    {
        base.OnMeetAnEnemy(enemy);
        bool willChasingThisEnemy = TmpUtil.RandomBool(0.3f);
        if (willChasingThisEnemy)
        {
            Chase(enemy);
        }
    }
    public override void OnDeath()
    {
        ChangeState(StopState);
        base.OnDeath();
        Invoke(nameof(OnDespawn), GameConstant.Character.BOT_REVIVE_TIME);
    }
    public override void OnDespawn()
    {
        base.OnDespawn();
        SimplePool.Despawn(this);
    }
    public override void WearClothes()
    {
        bool wearSkinNormal = Random.value > 0.1f;
        if (wearSkinNormal)
        {
            ChangeSkin(SkinType.SKIN_Normal);
            ChangeHat(Utilities.RandomEnumValue<HatType>());
            ChangeAccessory(Utilities.RandomEnumValue<AccessoryType>());
            ChangePant(Utilities.RandomEnumValue<PantType>());
        }
        else
        {
            ChangeSkin(Utilities.RandomEnumValue<SkinType>());
        }
        ChangeWeapon(Utilities.RandomEnumValue<WeaponType>());
    }
    void OnBulletComing(BulletPath bulletPath)
    {
        bool willDodge = TmpUtil.RandomBool(DodgingLevel);
        if (!willDodge)
        {
            return;
        }

        float dodgeQuality = Random.Range(DodgingLevel, 1);
        Vector3 dodgeDir = bulletPath.TF.right * (((TF.position - bulletPath.TF.position).x > 0) ? 1 : -1);
        dodgeDir = Quaternion.AngleAxis(TmpUtil.RandomSign() * 60 * (1 - dodgeQuality), Vector3.up) * dodgeDir;

        Vector3 expectedDodgePos = TF.position + Random.Range(2f, 3f) * dodgeDir;
        if (
            NavMesh.SamplePosition(expectedDodgePos, out NavMeshHit hit, float.PositiveInfinity, NavMesh.AllAreas) ||
            NavMesh.SamplePosition(2 * TF.position - expectedDodgePos, out hit, float.PositiveInfinity, NavMesh.AllAreas)
        )
        {
            DodgeTo(hit.position);
        }
        else
        {
            OnBulletComing(bulletPath);
        }
    }
    public void SetWasAimed(bool wasAimed)
    {
        reticle.SetActive(wasAimed);
    }
    public bool ChaseARandomTarget()
    {
        if (TmpLevelManager.Ins.PlayingCharacter.Count < 2)
        {
            return false;
        }
        Chase(TmpUtil.Choice(TmpLevelManager.Ins.PlayingCharacter, this));
        return true;
    }
    public void DodgeTo(Vector3 pos)
    {
        DodgeState.Destination = pos;
        ChangeState(DodgeState);
    }
    public void Chase(AbsCharacter enemy)
    {
        ChasingTarget = enemy;
        ChangeState(ChaseState);
    }
    public void Roam()
    {
        ChasingTarget = null;
        ChangeState(RoamState);
    }
    public void RoamOrChase()
    {
        bool willChase = TmpUtil.RandomBool(1/4f);
        if (willChase && ChaseARandomTarget())
        {
            return;
        }
        Roam();
    }
}
