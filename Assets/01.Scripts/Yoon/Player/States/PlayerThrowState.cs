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
        // 추후 여러 발 발사하는 기능 추가할 때 throwCount보다 현재 발사횟수가 넘으면 Idle로 가는 조건 추가

        // 애니메이션 끝나면 Idle로
        if (false == isAnimPlaying)
        {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        StopAnimation(animBoolHash);
    }
}
