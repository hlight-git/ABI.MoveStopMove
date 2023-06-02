public class BotMoveState : AbsState<Bot>
{
    public BotMoveState(Bot unit) : base(unit)
    {
    }

    public override void OnEnter()
    {
        //unit.Agent.enabled = true;
        unit.Agent.isStopped = false;
        unit.ChangeAnim(Constant.Anim.Ranger.RUN);
    }
    public override void OnExecute()
    {
        if (unit.IsDodging)
        {
            if (unit.IsReachedDestination())
            {
                unit.IsDodging = false;
            }
            return;
        }
        if (unit.ChasingTarget == null)
        {
            if (unit.IsReachedDestination())
            {
                Stop();
            }
            return;
        }
        if (unit.ChasingTarget != null)
        {
            if (unit.ChasingTarget.IsDead)
            {
                unit.RoamOrChase();
            }
            else if (unit.IsEnemyInRange(unit.ChasingTarget))
            {
                Stop();
            }
            else
            {
                unit.FollowTheTarget();
            }
        }
    }
    public override void OnExit()
    {
        unit.SetDestination(unit.TF.position);
        unit.Agent.isStopped = true;
        //unit.Agent.enabled = false;
    }
    void Stop()
    {
        unit.ChangeState(unit.StopState);
    }
}
