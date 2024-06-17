using System;
using System.Collections.Generic;
using UnityEngine;

public enum GameStateType
{
	Select,
	Battle,
	//Boss,
}

public class GameSystem : MonoBehaviour
{
	[SerializeField] private GameStateType startGameState;
	private GameStateType currentStateType;
	public GameStateType CurrentStateType => currentStateType;

	private Dictionary<GameStateType, InGameState> gameStates = new();

	private GameData gameData;
	public GameData GameData => gameData;

	// gameData 생성, 게임 상태 초기화
	public void Awake()
	{
		gameData = new GameData();

		foreach (GameStateType type in Enum.GetValues(typeof(GameStateType)))
		{
			InGameState gameState = GetComponent($"{type}State") as InGameState;
			if (gameState)
			{
				gameStates.Add(type, gameState);
			}
		}
	}

	public InGameState GetGameState(GameStateType gameStateType)
	{
		return gameStates[gameStateType];
	}

	public void BeginGame()
	{
		currentStateType = 0;

		// 모든 게임 상태 초기화
		foreach (var fair in gameStates)
		{
			fair.Value.Init();
		}

		gameStates[currentStateType].EnterState();
	}

	public void ExitGame()
	{
		foreach (var fair in gameStates)
		{
			fair.Value.ExitGame();
		}
	}

	public void ChangeState()
	{
		GameStateType[] Arr = (GameStateType[])Enum.GetValues(typeof(GameStateType));
		int nextTypeIndex = Array.IndexOf(Arr, currentStateType) + 1;
		GameStateType nextState = (Arr.Length == nextTypeIndex) ? Arr[0] : Arr[nextTypeIndex];

		gameStates[currentStateType].ExitState();
		currentStateType = nextState;
		gameStates[currentStateType].EnterState();
	}
}
