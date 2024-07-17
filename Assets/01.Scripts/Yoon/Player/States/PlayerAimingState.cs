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

        // 옳지 않은 조준 상태면 Idle로
        bool isAiming = playerAttack.Aiming(dragStartPosition, dragCurrentPosition + dragPositionOffset);
        if (false == isAiming)
        {
            stateMachine.ChangeState(PlayerStateEnum.Idle);
        }

        // 옳은 조준 상태이고, 발사 입력이 들어왔다면 Throw로
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
