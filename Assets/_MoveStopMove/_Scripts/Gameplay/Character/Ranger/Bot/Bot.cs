using UnityEngine;
using UnityEngine.AI;

public class Bot : AbRanger<Bot>
{
    [Header("Bot properties:")]
    [Range(0f, 1f), SerializeField] float dodginglevel;
    [SerializeField] protected GameObject reticle;

    public NavMeshAgent Agent;
    public ICharacter ChasingTarget;

    public float DodgingLevel {
        get => dodginglevel;
        set => dodginglevel = value;
    }
    
    public Vector3 destination;
    public bool IsDodging { get; set; }
    protected override void InitStates()
    {
        MoveState = new BotMoveState(this);
        StopState = new BotStopState(this);
    }
    public override void OnSpawn()
    {
        CancelInvoke();
        base.OnSpawn();
        SetName(Util.Choice(Constant.Prototype.BOT_NAMES));
        Agent.enabled = true;
        Agent.Warp(TF.position);
    }
    public override void OnHittedBy(Bullet bullet)
    {
        base.OnHittedBy(bullet);
        Agent.enabled = false;
    }
    public override void OnAnEnemyGetInRange(ICharacter enemy)
    {
        base.OnAnEnemyGetInRange(enemy);
        bool willChasingThisEnemy = Util.RandomBool(0.3f);
        if (willChasingThisEnemy)
        {
            Chase(enemy);
        }
    }
    public override void OnDeath()
    {
        base.OnDeath();
        Invoke(nameof(OnDespawn), Constant.Ranger.BOT_REVIVE_TIME);
    }
    public override void OnDespawn()
    {
        body.OnDespawn();
        base.OnDespawn();
        SimplePool.Despawn(this);
    }
    public override void SpawnBody()
    {
        CreateRandomBody();
        base.SpawnBody();
    }
    protected void CreateRandomBody()
    {
        weaponType = Util.Enum.Choice<WeaponType>();
        bool getBodySet = Util.RandomBool(0.1f);
        if (getBodySet)
        {
            skinType = Util.Enum.Choice(SkinType.SKIN_Normal);
            return;
        }
        skinType = SkinType.SKIN_Normal;
        hatType = Util.Enum.Choice<HatType>();
        pantType = Util.Enum.Choice<PantType>();
        accessoryType = Util.Enum.Choice<AccessoryType>();
    }
    public void OnBulletComing(BulletPath bulletPath)
    {
        bool willDodge = Util.RandomBool(DodgingLevel);
        if (!willDodge)
        {
            return;
        }

        float dodgeQuality = Random.Range(DodgingLevel, 1);
        Vector3 dodgeDir = bulletPath.TF.right * (((TF.position - bulletPath.TF.position).x > 0) ? 1 : -1);
        dodgeDir = Quaternion.AngleAxis(Util.RandomSign() * 60 * (1 - dodgeQuality), Vector3.up) * dodgeDir;

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
        if (LevelManager.Ins.PlayingCharacter.Count < 2)
        {
            return false;
        }
        Chase(Util.Choice(LevelManager.Ins.PlayingCharacter, this));
        return true;
    }
    public bool IsReachedDestination()
    {
        Vector3 distance = TF.position - destination;
        distance.y = 0;
        return distance.sqrMagnitude < 0.1f;
    }
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        this.destination.y = TF.position.y;
        Agent.SetDestination(destination);
    }
    public void FollowTheTarget()
    {
        SetDestination(ChasingTarget.TF.position);
    }

    public void DodgeTo(Vector3 pos)
    {
        if (IsDead) { return; }
        IsDodging = true;
        ChangeState(MoveState);
        SetDestination(pos);
    }
    public void Chase(ICharacter enemy)
    {
        if (IsDead) { return; }
        ChasingTarget = enemy;
        ChangeState(MoveState);
    }
    public void Roam()
    {
        if (IsDead) { return; }
        ChasingTarget = null;
        ChangeState(MoveState);
        SetDestination(LevelManager.Ins.RandomPoint());
    }
    public void RoamOrChase()
    {
        if (IsDead) { return; }
        bool willChase = Util.RandomBool(1 / 4f);
        if (willChase && ChaseARandomTarget())
        {
            return;
        }
        Roam();
    }

    public override void SetMoveSpeed(float moveSpeed)
    {
        Agent.speed = moveSpeed;
        Agent.acceleration = moveSpeed * 6;
    }
}
