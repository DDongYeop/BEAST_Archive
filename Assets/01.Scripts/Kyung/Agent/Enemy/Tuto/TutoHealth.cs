using FSM;
using UnityEngine;

public class TutoHealth : AgentHealth
{
    private FSMRunner _fsmRunner;

    protected override void Awake()
    {
        base.Awake();

        _fsmRunner = GetComponent<FSMRunner>();
    }

    public override void OnDamage(int damage, Vector3 hitPos)
    {
        base.OnDamage(damage, hitPos);
        
        if (_fsmRunner.GetCurrentAction().IsAttackStop)
            _fsmRunner.ChangeState(FSMState.Idle);
    }
}
