using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float dragSpeed;
	private GameManager _gameManager;
	public MaskBehaviour activeMask;
	public PlayerState State { get; private set; }

	public event Action<MaskBehaviour> MaskSelected;

	public void Initialize(GameManager gameManager)
	{
		_gameManager = gameManager;
	}


	private void ManageIdleInput()
	{
		if (Input.GetMouseButton(0))
		{
			var eventData = new PointerEventData(EventSystem.current);
			eventData.position = Input.mousePosition;
			var results = new List<RaycastResult>();
			EventSystem.current.RaycastAll(eventData, results);

			if (!results.Any())
				return;

			var masks = results.Select(x => x.gameObject.GetComponent<MaskBehaviour>());
			var mask = masks.FirstOrDefault(x => x != null);
			if (!mask == null)
				return;

			State++;
			MaskSelected?.Invoke(mask);
			activeMask = mask;
		}
	}

	private void ManageDraggingInput()
	{
		if (Input.GetMouseButtonUp(0))
		{
			if (_gameManager.maskManager.StopDrag())
			{
				State = PlayerState.Disabled;
				return;
			}

			State = PlayerState.Idle;
			return;
		}

		var delta = Input.mousePositionDelta;
		_gameManager.maskManager.Drag(delta);
	}


	public void AnsweringUpdate()
	{
		switch (State)
		{
			case PlayerState.Idle:
				ManageIdleInput();
				break;

			case PlayerState.Dragging:
				ManageDraggingInput();
				break;
		}
	}

	public void StartAnswering()
	{
		State = PlayerState.Idle;
	}
}