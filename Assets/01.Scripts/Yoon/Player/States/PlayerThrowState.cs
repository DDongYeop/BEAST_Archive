public class PlayerThrowState : PlayerState
{
    public PlayerThrowState(PlayerController playerController, PlayerStateMachine stateMachine, string animationBoolName) : base(playerController, stateMachine, animationBoolName) { }

    public override void Enter()
    {
        PlayAnimation(animBoolHash);
        playerAttack.Throw();
    }

    public override void UpdateState()
    {
        // �ִϸ��̼� ������ Idle��
        if (false == isAnimPlaying)
        {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
        playerInput.IsThrowReady = true;
    }

    public override void Exit()
    {
        StopAnimation(animBoolHash);
    }
}
