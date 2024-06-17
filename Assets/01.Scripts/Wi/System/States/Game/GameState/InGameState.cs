using UnityEngine;

public abstract class InGameState : MonoBehaviour
{
    protected GameSystem gameSystem;

	protected virtual void Awake()
	{
		gameSystem = GetComponent<GameSystem>();
	}

	public abstract void Init();        // 게임 상태로 변경 되었을 때, 1회 실행되는 함수
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void ExitGame();    // 게임 상태에서 다른 상태로 변경되었을 때, 1회 실행되는 함수

    protected void NextState()
	{
        gameSystem.ChangeState();
	}
}
