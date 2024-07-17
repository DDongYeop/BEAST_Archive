public class PlayerBowAmingState : PlayerAimingState
{
    public PlayerBowAmingState(PlayerController playerController, PlayerStateMachine stateMachine, string animationBoolName) : base(playerController, stateMachine, animationBoolName) { }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        dragCurrentPosition = playerInput.GetTouchPosition();

        // 옳지 않은 조준 상태면 Idle로
        bool isAiming = playerAttack.Aiming(dragStartPosition, dragCurrentPosition + dragPositionOffset);
        if (false == isAiming)
        {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

        // 옳은 조준 상태라면 BowThrow로
        if (false == playerInput.IsThrowReady)
        {
            stateMachine.ChangeState(PlayerStateEnum.BowThrow);
        }
    }

    public override void Exit() 
    {
        base.Exit();
    }
}
