using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
		InstantiateTargets();
	}

	private void InstantiateTargets()
	{
		var angle = 0;
		var type = TargetType.Type1;
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				var go = Instantiate(targetPrefab, this.transform);
				go.transform.position += Quaternion.Euler(0, 0, angle) * (Vector3.right * radius);
				angle += 180;

				TargetPlaceholder targetPlaceholder = go.GetComponent<TargetPlaceholder>();
				targetPlaceholder.type = type;
				targets.Add(targetPlaceholder);
			}

			angle += 45;
			type++;

		}
	}

	private Coroutine returningCoroutine;

	public void Drag(Vector3 delta)
	{
		selector.transform.DOKill();

		selector.transform.position += delta * draggingSpeed;
		var maxDistance = radius * 1.2f;
		
		selector.transform.position = Vector3.ClampMagnitude(selector.transform.position - center.position, maxDistance);
	}

	public void StopDrag()
	{
		Debug.Log("DraggingStopped");

		selector.transform.DOMove(center.position, returnDuration);
		selector.transform.DOPlay();
	}
}