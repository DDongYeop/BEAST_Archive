using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController playerController;
    protected PlayerInput playerInput;
    protected PlayerAttack playerAttack;

    // ����� �ִϸ��̼� Ʈ�������� Bool Property Hash
    protected int animBoolHash;

    // ����ϰ� �ִ���
    protected bool isAnimPlaying;

    public PlayerState(PlayerController playerController, PlayerStateMachine stateMachine, string animationBoolName)
    {
        this.playerController = playerController;
        this.stateMachine = stateMachine;
        animBoolHash = Animator.StringToHash(animationBoolName);
        playerInput = playerController.PlayerInput;
        playerAttack = playerController.PlayerAttack;
    }

    //���¿� ������ �� ���� ��
    public virtual void Enter() { }

    //���¿� �ִ� ���� ������� ��
    public virtual void UpdateState() { }

    //���¸� ���� �� ���� ��
    public virtual void Exit() { }

    #region Animation

    public void PlayAnimation(int hashValue)
    {
        playerController.Animator.SetBool(hashValue, true);
        isAnimPlaying = true;
    }

    public void StopAnimation(int hashValue)
    {
        playerController.Animator.SetBool(hashValue, false);
        isAnimPlaying = false;
    }

    public void SetAnimationParameterValue(int hashValue, float value)
    {
        playerController.Animator.SetFloat(hashValue, value);
    }

    public void SetAnimationParameter(int hashValue, int value)
    {
        playerController.Animator.SetInteger(hashValue, value);
    }

    public void SetAnimationParameter(int hashValue)
    {
        playerController.Animator.SetTrigger(hashValue);
    }

    public void AnimationEndTrigger()
    {
        isAnimPlaying = false;
    }

    #endregion
}