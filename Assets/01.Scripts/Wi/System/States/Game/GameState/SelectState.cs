using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectState : InGameState
{
	private StageSelectManager stageSelectManager;

	protected override void Awake()
	{
		base.Awake();
		stageSelectManager = GetComponentInChildren<StageSelectManager>();
	}

	public override void Init()
	{
		stageSelectManager.Init();
	}

	public override void EnterState()
	{
		stageSelectManager.Active(true);
	}

	public override void ExitState()
	{
		stageSelectManager.Active(false);
	}

	public override void ExitGame()
	{

	}

	public new void NextState()
	{
		//if (game)

		base.NextState();
	}
}
