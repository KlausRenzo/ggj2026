using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Serialization;

public class MaskManager : MonoBehaviour
{
	[FormerlySerializedAs("draggingSpeed")] [SerializeField]
	private float baseDraggingSpeed;

	[SerializeField] private float malusDraggingSpeed = 1;
	private Vector3 showPosition;
	private Vector3 hidePosition;

	[FormerlySerializedAs("activeMask")] [SerializeField]
	private MaskBehaviour selector;

	private GameManager _gameManager;

	[SerializeField] private Transform center;
	[SerializeField] private float returnDuration;
	[SerializeField] private List<TargetPlaceholder> targets;
	[SerializeField] private float radius = 100;
	[SerializeField] private GameObject targetPrefab;
	public MaskBehaviour cursor;

	public void Initialize(GameManager gameManager)
	{
		_gameManager = gameManager;
	}

	private void Awake()
	{
		targets = this.GetComponentsInChildren<TargetPlaceholder>().ToList();
		showPosition = this.transform.position;
		RectTransform rectTransform = ((RectTransform) transform);
		float width = (rectTransform.anchorMax.x - rectTransform.anchorMin.x) * Screen.width;
		hidePosition = showPosition + new Vector3(width, 0, 0);

		this.transform.position = hidePosition;
	}

	[SerializeField] private float dropDistance = 10;
	[SerializeField] private int[] steps = new int[3] {7, 14, 21};

	public TweenerCore<Vector3, Vector3, VectorOptions> Enable()
	{
		Debug.Log("MaskManager Enable");
		var score = _gameManager.bonusScore;

		var radius = _gameManager.activeBlock.maskRadius;
		foreach (var target in targets)
		{
			switch (Math.Abs((int) target.state))
			{
				case 0:
				case 1:
					target.isEnabled = true;
					break;

				case 2:
					target.isEnabled = score >= steps[0];
					target.gameObject.SetActive(score >= steps[0]);
					break;

				case 3:
					target.isEnabled = score >= steps[1];
					target.gameObject.SetActive(score >= steps[1]);
					break;

				case 4:
					target.isEnabled = score >= steps[2];
					target.gameObject.SetActive(score >= steps[2]);
					break;
			}

			target.transform.localPosition = target.transform.localPosition.normalized * radius;
		}

		return this.transform.DOMove(showPosition, 1).Play();
	}


	public TweenerCore<Vector3, Vector3, VectorOptions> Disable()
	{
		return this.transform.DOMove(hidePosition, 1).Play();
	}

	public void Drag(Vector3 delta)
	{
		selector.transform.DOKill();

		selector.transform.position += delta * (baseDraggingSpeed - _gameManager.burnout * malusDraggingSpeed);
		var maxDistance = radius * 1.2f;

		selector.transform.localPosition = Vector3.ClampMagnitude(selector.transform.localPosition, maxDistance);
	}

	public TargetPlaceholder StopDrag()
	{
		TargetPlaceholder nearest = targets.OrderBy(x => (x.transform.position - selector.transform.position).magnitude).First();
		var distance = (nearest.transform.position - selector.transform.position).magnitude;
		Debug.Log(distance);
		if (distance < dropDistance)
		{
			selector.transform.DOMove(nearest.transform.position, 0.25f).Play();
			return nearest;
		}

		ResetPosition();
		return null;
	}

	public void ResetPosition()
	{
		selector.transform.DOMove(center.position, returnDuration).Play();
	}
}