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

        // ���� ���� ���� ���¸� Idle��
        bool isAiming = playerAttack.Aiming(dragStartPosition, dragCurrentPosition + dragPositionOffset);
        if (false == isAiming)
        {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

        // ���� ���� ���¶�� BowThrow��
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
