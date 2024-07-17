using UnityEngine;

public class PlayerAimingState : PlayerState
{
    public PlayerAimingState(PlayerController playerController, PlayerStateMachine stateMachine, string animationBoolName) : base(playerController, stateMachine, animationBoolName) { }

    protected Vector3 dragStartPosition;
    protected Vector3 dragCurrentPosition;

    protected Vector3 dragPositionOffset = new Vector3(-0.75f, -2.5f, 0);

    public override void Enter()
    {
        dragStartPosition = playerInput.GetTouchPosition();
        PlayAnimation(animBoolHash);
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

        // ���� ���� �����̰�, �߻� �Է��� ���Դٸ� Throw��
        if (false == playerInput.IsThrowReady)
        {
            stateMachine.ChangeState(PlayerStateEnum.Throw);
        }
    }

    public override void Exit()
    {
        playerAttack.EndAiming();
        StopAnimation(animBoolHash);
    }
    
}
