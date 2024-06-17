using System;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoSingleton<SystemManager>
{
	private delegate bool CanChangeToState();

	[Header("Properties")]
    [SerializeField] private STATE_TYPE _currentState = STATE_TYPE.None;
    public STATE_TYPE CurrentState => _currentState;

	private Dictionary<STATE_TYPE, CanChangeToState> _checkChangeable = new();

	[HideInInspector] public SystemData Data = new ();
	private event Action<STATE_TYPE> OnStateChange;

	private void Awake()
	{
		_checkChangeable.Add(STATE_TYPE.Menu, ChangeToMenu);
		_checkChangeable.Add(STATE_TYPE.Game, ChangeToGame);
	}

	private bool ChangeToMenu() => true;

	private bool ChangeToGame() => Data.stage != null;

	/// <summary>
	/// 상태 전환을 담당하는 함수
	/// </summary>
	/// <param name="newState">변경할 상태</param>
	/// <returns>전환 성공 여부</returns>
	public bool ChangeState(STATE_TYPE newState)
	{
		if (_currentState == newState)
			return false;

		if (_checkChangeable[newState].Invoke())
		{
			_currentState = newState;
			OnStateChange?.Invoke(_currentState);
			return true;
		}

		return false;
	}

	public void SubscribeEvent(Action<STATE_TYPE> action)
	{
		OnStateChange += action;
	}

	public void UnsubscribeEvent(Action<STATE_TYPE> action)
	{
		OnStateChange -= action;
	}
}
