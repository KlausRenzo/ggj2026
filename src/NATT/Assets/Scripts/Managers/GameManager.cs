using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public PlayerController player;
	public MaskManager maskManager;
	public CrowdManager crowdManager;

	public Camera gameCamera;
	public Camera maskCamera;
	[SerializeField] private GameState _state;

	public GameState State
	{
		get => _state;
		set => _state = value;
	}

	private IEnumerator Start()
	{
		player.Initialize(this);
		maskManager.Initialize(this);
		crowdManager.Initialize(this);

		yield return new WaitForSeconds(2);
		NextState();
	}

	private void Update()
	{
		Debugger();
		Loop();
	}

	private void Debugger()
	{
		if (Input.GetKeyDown(KeyCode.P))
		{
			NextState();
		}
	}

	private void Loop()
	{
		switch (State)
		{
			case GameState.SelectingNpc:

				break;

			case GameState.Answering:
				player.AnsweringUpdate();
				break;

			case GameState.GettingFeedback:
				break;

			case GameState.Idle:
				return;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}


	private void NextState()
	{
		switch (State)
		{
			case GameState.Answering:
				State = GameState.Idle;
				maskManager.Disable()
							.OnComplete(() => State = GameState.GettingFeedback);
				break;

			case GameState.GettingFeedback:
				State = GameState.Idle;

				crowdManager.SendNewNpc()
							.OnComplete(() => State = GameState.SelectingNpc)
							.OnComplete(NextState);
				break;

			case GameState.SelectingNpc:
				State = GameState.Idle;
				DOTween.Sequence()
						.Append(maskManager.Enable())
						.OnComplete(player.StartAnswering)
						.OnComplete(NextState)
						.Play();
				break;
			default:	
				throw new ArgumentOutOfRangeException();
		}
	}
	
	

	public void OnMaskDropped(TargetPlaceholder selectedTarget)
	{
	}
}