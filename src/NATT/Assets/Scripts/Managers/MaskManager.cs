using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Serialization;

public class MaskManager : MonoBehaviour
{
	[SerializeField] private float draggingSpeed;

	[FormerlySerializedAs("activeMask")] [SerializeField]
	private MaskBehaviour selector;

	private GameManager _gameManager;

	[SerializeField] private Transform center;
	[SerializeField] private float returnDuration;
	[SerializeField] private List<TargetPlaceholder> targets;
	[SerializeField] private float radius = 100;

	[SerializeField] private GameObject targetPrefab;

	public void Initialize(GameManager gameManager)
	{
		_gameManager = gameManager;
	}

	private void Awake()
	{
		targets = this.GetComponentsInChildren<TargetPlaceholder>().ToList();
	}

	[SerializeField] private float dropDistance = 10;

	public TweenerCore<Vector3, Vector3, VectorOptions> Enable()
	{
		this.transform.localPosition = new Vector3(400, 0, 0);
		return this.transform.DOLocalMove(Vector3.zero, 1).Play();
	}

	public TweenerCore<Vector3, Vector3, VectorOptions> Disable()
	{
		return this.transform.DOLocalMove(new Vector3(400, 0, 0), 1).Play();
	}

	public void Drag(Vector3 delta)
	{
		selector.transform.DOKill();

		selector.transform.position += delta * draggingSpeed;
		var maxDistance = radius * 1.2f;

		selector.transform.localPosition = Vector3.ClampMagnitude(selector.transform.localPosition, maxDistance);
	}

	public bool StopDrag()
	{
		var nearest = targets.OrderBy(x => (x.transform.position - selector.transform.position).magnitude).First();
		var distance = (nearest.transform.position - selector.transform.position).magnitude;
		Debug.Log(distance);
		if (distance < dropDistance)
		{
			_gameManager.OnMaskDropped(nearest);
			selector.transform.DOMove(nearest.transform.position, 0.25f).Play();
			return true;
		}

		selector.transform.DOMove(center.position, returnDuration).Play();
		return false;
	}
}