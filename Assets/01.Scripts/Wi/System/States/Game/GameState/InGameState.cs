using UnityEngine;

public abstract class InGameState : MonoBehaviour
{
    protected GameSystem gameSystem;

	protected virtual void Awake()
	{
		gameSystem = GetComponent<GameSystem>();
	}

	public abstract void Init();        // ���� ���·� ���� �Ǿ��� ��, 1ȸ ����Ǵ� �Լ�
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void ExitGame();    // ���� ���¿��� �ٸ� ���·� ����Ǿ��� ��, 1ȸ ����Ǵ� �Լ�

    protected void NextState()
	{
        gameSystem.ChangeState();
	}
}
